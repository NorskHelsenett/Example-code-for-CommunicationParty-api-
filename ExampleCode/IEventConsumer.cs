using System.Text.Json;

namespace ExampleCode;

public interface IEventConsumer
{
    
    /// <summary>
    /// Method that handles a CommunicationPartyCreated event. 
    /// </summary>
    /// <param name="comParty"></param>
    void CommunicationPartyCreated(JsonElement comParty);
    
    /// <summary>
    /// Method that handles a CommunicationPartyUpdated event. 
    /// </summary>
    /// <param name="comParty"></param>
    void CommunicationPartyUpdated(JsonElement comParty);
}