using System.Text.Json;

namespace ExampleCode.DTO;

public class CommunicationPartyExportResult
{
    /// <summary>
    /// DateTimeOffset of last consumed event for export. Can be used to get new events from CommunicationPartyEvents.
    /// </summary>
    public DateTimeOffset LastDateTimeOffset { get; set; }

    /// <summary>
    /// Id of last consumed event for export. Can be used to get new events from CommunicationPartyEvents.
    /// </summary>
    public string LastEventId { get; set; }

    /// <summary>
    /// List of CommunicationParties
    /// </summary>
    public List<JsonElement> CommunicationParties { get; set; }
}