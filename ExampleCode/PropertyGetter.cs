using System.Globalization;
using System.Text.Json;

namespace ExampleCode;

public static class PropertyGetter
{
    /// <summary>
    ///  Gets the name of a communication party.
    /// </summary>
    /// <param name="communicationParty">A communication party object represented as a JsonElement.</param>
    public static string? GetName(JsonElement communicationParty)
    {
        return communicationParty.GetProperty("Name").GetString();
    }

    /// <summary>
    ///  Gets the herId of a communication party.
    /// </summary>
    /// <param name="communicationParty">A communication party object represented as a JsonElement.</param>
    public static int GetHerId(JsonElement communicationParty)
    {
        return communicationParty.GetProperty("HerId").GetInt32();
    }
    
    /// <summary>
    ///  Gets the 'To' property from the Valid Period of a communication party.
    /// </summary>
    /// <param name="communicationParty">A communication party object represented as a JsonElement.</param>
    private static string? GetValidTo(JsonElement communicationParty)
    {
       return communicationParty.GetProperty("Valid").GetProperty("To").GetString();
    }
    
    /// <summary>
    ///  Gets the Active status of a communication party
    /// </summary>
    /// <param name="communicationParty">A communication party object represented as a JsonElement.</param>
    public static bool IsActive (JsonElement communicationParty)
    {
        var success = DateTime.TryParse(GetValidTo(communicationParty), CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate);
        
        if (success)
        {
            return parsedDate > DateTime.Now;
        }
        
        return false;
    }
}