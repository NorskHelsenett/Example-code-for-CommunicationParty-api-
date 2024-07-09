using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace ExampleCode;

public class DummyHealthCareSystem(ILogger<DummyHealthCareSystem> logger) : IEventConsumer
{
    private readonly ILogger _logger = logger;

    public void CommunicationPartyCreated(JsonElement comParty)
    {
        
        _logger.LogInformation("DummyHealthCareSystem has received a push notification about HerID: {HerId} {Name} has been created", 
            PropertyGetter.GetHerId(comParty),
            PropertyGetter.GetName(comParty));
    }

    public void CommunicationPartyUpdated(JsonElement comParty)
    {
        _logger.LogInformation("DummyHealthCareSystem has received a push notification about HerID: {HerId} {Name} has been updated.", 
            PropertyGetter.GetHerId(comParty),
            PropertyGetter.GetName(comParty));
    }
}