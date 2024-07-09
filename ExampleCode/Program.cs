using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ExampleCode;
using ExampleCode.HttpClients;


var builder = Host.CreateDefaultBuilder(args)
    .ConfigureLogging(logging =>
    {
        logging.ClearProviders();
        // add console as logging target
        logging.AddConsole();
        // add debug output as logging target
        logging.AddDebug();
        // set minimum level to log
        logging.SetMinimumLevel(LogLevel.Debug);
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient("ExportAPIClient",
            config => { config.BaseAddress = new Uri("https://cpe.test.grunndata.nhn.no/CommunicationPartyExport"); });
        services.AddHttpClient("EventApiClient",
            config => { config.BaseAddress = new Uri("https://cpe.test.grunndata.nhn.no/CommunicationPartyEvents"); });
        services.AddSingleton<CpExportClient>();
        services.AddSingleton<CpEventClient>();
        services.AddSingleton<CommunicationPartyLookup>();
        services.AddSingleton<DummyHealthCareSystem>();
        services.AddHostedService<CommunicationPartyLookupBackgroundService>();
    });

var app = builder.Build();
app.Run();