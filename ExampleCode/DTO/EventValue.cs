using System.Text.Json;

namespace ExampleCode.DTO;

/// <summary>
/// Object that contains the event information
/// </summary>
public class EventValue
{
    /// <summary>
    /// Id of the event
    /// </summary>
    public string EventId { get; set; }

    /// <summary>
    /// Datetime for when the event was created
    /// </summary>
    public DateTimeOffset FromDatetime { get; set; }

    /// <summary>
    /// HerId
    /// </summary>
    public int HerId { get; set; }

    /// <summary>
    /// Organization number
    /// </summary>
    public int OrgNr { get; set; }

    /// <summary>
    /// Type of event - CommunicationPartyUpdated / CommunicationPartyCreated
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// The event itself
    /// </summary>
    public JsonElement CommunicationParty { get; set; }
}