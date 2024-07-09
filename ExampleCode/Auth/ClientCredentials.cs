
namespace ExampleCode.Auth;

public static class ClientCredentials
{
    
    /*
     For access control the CommunicationParty api uses HelseId.
     To use helseId you have to sing in to 'Selvbetjening' and create a helseId client.    
     Instructions and documentation for how to use HelseID can be found here:: https://utviklerportal.nhn.no/informasjonstjenester/helseid/
     Example code for implementation and use of HelseID can be found here: https://github.com/NorskHelsenett/HelseID.Samples  
    */
    
    public static string GetJwt()
    {
        return "AccessToken";
    }
}