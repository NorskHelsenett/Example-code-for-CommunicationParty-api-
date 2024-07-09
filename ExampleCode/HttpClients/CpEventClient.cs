using System.Net.Http.Headers;
using System.Text.Json;
using ExampleCode.Auth;
using ExampleCode.DTO;

namespace ExampleCode.HttpClients;

public class CpEventClient(IHttpClientFactory httpClientFactory)
{
   
    private readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };
    private readonly HttpClient _httpClient = httpClientFactory.CreateClient("EventApiClient");
    
    private void AddAuthorizationHeader()
    {
        var token = ClientCredentials.GetJwt();
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
    }

    /// <summary>
    /// Calls the event endpoint with default page size and receives events after the inserted <paramref name="eventId"/>.
    /// </summary>
    /// <param name="eventId">The guid of the last event we have processed.</param>
    /// <returns>The response object from the events endpoint.</returns>
    public async Task<EventResponse> GetEventsAsync(string? eventId)
    {
        AddAuthorizationHeader();

        var request = new HttpRequestMessage(
            HttpMethod.Get,
            $"CommunicationPartyEvents?EventId={eventId}");

        var response = await _httpClient.SendAsync(request);

        response.EnsureSuccessStatusCode();

        return JsonSerializer.Deserialize<EventResponse>(await response.Content.ReadAsStringAsync(), _options);
    }
    
}