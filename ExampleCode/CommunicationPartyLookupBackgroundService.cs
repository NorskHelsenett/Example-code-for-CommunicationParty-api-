using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace ExampleCode;

public class CommunicationPartyLookupBackgroundService(
    ILogger<CommunicationPartyLookupBackgroundService> log,
    CommunicationPartyLookup communicationPartyLookup)
    : BackgroundService
{
    private readonly ILogger _logger = log;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
         _logger.LogInformation("My CommunicationPartyLookupBackgroundService starting....");
          await communicationPartyLookup.InitializeLocalArCacheAndGetLastEventId();
        
         while (!stoppingToken.IsCancellationRequested)
         {
             await CheckForUpdateAsync();
             await Task.Delay(10000, stoppingToken); // Wait 10 second
         }
         
         _logger.LogInformation("Background Service is stopping.");
    }

    private async Task CheckForUpdateAsync()
    {
        try
        {
            await communicationPartyLookup.PullAndProcessUpdates(communicationPartyLookup.LastEventId);
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occurred: {e}");
        }
    }
}