using System.Net.Http.Headers;
using System.Text.Json;
using ExampleCode.Auth;
using ExampleCode.DTO;

namespace ExampleCode.HttpClients;

public class CpExportClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
 
    public CpExportClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
  
    }
    
    /// <summary>
    /// Calls the export endpoint with streaming enabled and adds all communication parties to the activeCommunicationPartyRepository,
    /// while also storing the lastEventId. Therefore, we know which event to use as a starting point when calling the event endpoint to
    /// update our local cache of communication parties with the latest data after consuming the export.
    /// </summary>
    public async Task<(Dictionary<int, JsonElement>? allCommuncationparties, string? lastEventId)> GetDictionaryOfCommunicationPartiesAsync()
    {
        var httpClient = _httpClientFactory.CreateClient("ExportAPIClient");
        var token =  GetToken();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var dictionary = new Dictionary<int, JsonElement>();
        var lastEventId = "";
        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"CommunicationPartyExport");
        
        using (var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
        {
            response.EnsureSuccessStatusCode();
            await using var stream = await response.Content.ReadAsStreamAsync();

            var communicationPartyExportResult = await JsonSerializer.DeserializeAsync<CommunicationPartyExportResult>(stream, _options);

            lastEventId = communicationPartyExportResult?.LastEventId;
            
            foreach (var cp in communicationPartyExportResult.CommunicationParties)
            {
                dictionary.Add(cp.GetProperty("HerId").GetInt32(), cp);
            }
        }

        return (dictionary, lastEventId);
    }
    
    private static string GetToken()
    {
        return ClientCredentials.GetJwt();
    }
}