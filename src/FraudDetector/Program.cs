using FraudDetector;
using Microsoft.AspNetCore;
using Serilog;

await BuildWebHost(args).RunAsync();

static IWebHost BuildWebHost(string[] args) =>
    WebHost
        .CreateDefaultBuilder(args)
        .UseStartup<Startup>()
        .UseSerilog((builderContext, config) =>
        {
            config
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.Console();
        })
        .Build();