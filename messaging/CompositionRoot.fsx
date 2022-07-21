// #r "nuget: NBB.Messaging.Host, 6.0.19"
#r "nuget: NBB.Messaging.Nats, 6.0.19"
#r "nuget: NBB.Messaging.Rusi, 6.0.19"
#r "nuget: Microsoft.Extensions.DependencyInjection, 6.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 6.0.1"
#r "nuget: Microsoft.Extensions.Configuration, 6.0.0"
#r "nuget: Microsoft.Extensions.Configuration.Json, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging.Console, 6.0.0"
// #r "nuget: Moq, 4.16.1"

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.Logging
open System.IO

let buildContainer servicesAction =
    let configuration =
        ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false)
            .AddEnvironmentVariables()
            .Build()

    let services = ServiceCollection()

    services.AddSingleton<IConfiguration>(configuration)
    |> ignore

    services.AddLogging (fun x ->
        x.AddConsole().SetMinimumLevel(LogLevel.Debug)
        |> ignore)
    |> ignore

    services
        .AddMessageBus()
        .AddRusiTransport(configuration)
    |> ignore

    servicesAction services configuration

    services.BuildServiceProvider()