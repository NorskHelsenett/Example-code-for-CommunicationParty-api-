namespace ExampleCode.DTO;

/// <summary>
/// Reponse object
/// </summary>
public class EventResponse
{
    /// <summary>
    /// List of events
    /// </summary>
    public List<EventValue> CommunicationParties { get; set; }

    /// <summary>
    /// Links
    /// </summary>
    public Links Links { get; set; }
}

/// <summary>
/// Url links
/// </summary>
public class Links
{
    public Url Next { get; set; }
}

/// <summary>
/// Url
/// </summary>
public class Url
{
    /// <summary>
    /// A url
    /// </summary>
    public string Href { get; set; }
}