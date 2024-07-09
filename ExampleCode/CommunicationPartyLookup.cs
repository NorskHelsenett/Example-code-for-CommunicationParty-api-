using System.Text.Json;
using ExampleCode.DTO;
using ExampleCode.HttpClients;
using Microsoft.Extensions.Logging;

namespace ExampleCode;

public class CommunicationPartyLookup : IEventConsumer
{
    private readonly CpExportClient _cpExportClient;
    private readonly CpEventClient _cpEventClient;
    private Dictionary<int, JsonElement> _repository;
    private readonly ILogger _logger;
    public string LastEventId;

    public CommunicationPartyLookup(CpExportClient cpExportClient, CpEventClient cpEventClient, ILogger<CommunicationPartyLookup> logger)
    {
        _cpExportClient = cpExportClient;
        _cpEventClient = cpEventClient;
        _logger = logger;
    }

    
    public async Task InitializeLocalArCacheAndGetLastEventId()
    {
        var dictionaryAndLastEventId = await _cpExportClient.GetDictionaryOfCommunicationPartiesAsync();
        _repository = dictionaryAndLastEventId.allCommuncationparties;
        LastEventId = dictionaryAndLastEventId.lastEventId;
    }
    
    /// <summary>
    /// Call event endpoint for new events and processes them.
    /// </summary>
    /// <param name="evenId"></param>
    public async Task PullAndProcessUpdates(string? evenId)
    {
        var eventResponse = await _cpEventClient.GetEventsAsync(evenId);
        if (eventResponse.CommunicationParties.Count == 0)
        {
            _logger.LogInformation("No updates found");
        }

        ProcessEvents(eventResponse.CommunicationParties);
    }

    /// <summary>
    /// Iterates over the list of <paramref name="eventValues"/> and updates the the dictionary with the latest data. 
    /// </summary>
    /// <param name="eventValues">A list of EventValue objects, each that represent an event</param>
    private void ProcessEvents(List<EventValue> eventValues)
    {
        foreach (var cpEvent in eventValues)
        {
            if (cpEvent.EventType.Equals("CommunicationPartyCreated"))
            {
                CommunicationPartyCreated(cpEvent.CommunicationParty);
                LastEventId = cpEvent.EventId;
                continue;
            }

            if (PropertyGetter.IsActive(cpEvent.CommunicationParty) &&
                CommunicationPartyExist(cpEvent.CommunicationParty))
            {
                CommunicationPartyUpdated(cpEvent.CommunicationParty);

                LastEventId = cpEvent.EventId;
            }
        }
    }

    public void CommunicationPartyCreated(JsonElement comParty)
    {
        _repository?.Add(comParty.GetProperty("HerId").GetInt32(), comParty);
    }


    public void CommunicationPartyUpdated(JsonElement comParty)
    {
        var herId = PropertyGetter.GetHerId(comParty);
        if (PropertyGetter.IsActive(comParty))
        {
            _logger.LogInformation(
                "HerID {HerId} {Name} has been updated and updated to communication party repository",
                herId, PropertyGetter.GetName(comParty));
            _repository[herId] = comParty;
        }
        else
        {
            _repository.Remove(herId);
            _logger.LogInformation(
                "HerID {HerId} {Name} has been removed from the communication party repository because it has been deactivated",
                herId, PropertyGetter.GetName(comParty));
        }
    }

    private bool CommunicationPartyExist(JsonElement comParty)
    {
        return _repository.TryGetValue(PropertyGetter.GetHerId(comParty), out _);
    }
}