#r "nuget: NBB.Messaging.Nats, 6.0.22"
#r "nuget: NBB.Messaging.Rusi, 6.0.22"
#r "nuget: NBB.Messaging.Host, 6.0.22"
#r "nuget: Microsoft.Extensions.DependencyInjection, 6.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 6.0.1"
#r "nuget: Microsoft.Extensions.Configuration, 6.0.0"
#r "nuget: Microsoft.Extensions.Configuration.Json, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging.Console, 6.0.0"

#r "nuget: Moq, 4.16.1"

#load "CompositionRoot.fsx"

open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.Options
open Microsoft.Extensions.Configuration
open NBB.Messaging.Host
open Moq
open System


let buildContainer () =
    CompositionRoot.buildContainer (fun services configuration ->
        services.AddSingleton<IHostApplicationLifetime>(Mock.Of<IHostApplicationLifetime>())
        |> ignore

        services.AddMessagingHost(
            configuration,
            fun h ->
                h.Configure (fun configBuilder ->
                    configBuilder
                        .AddSubscriberServices(fun c ->
                            for i in 1..1000 do
                                c.FromTopic(sprintf "MyCommand_%i" i) |> ignore)
                        .WithDefaultOptions()
                        .UsePipeline(fun pipelineBuilder ->
                            pipelineBuilder.UseExceptionHandlingMiddleware()
                            // .Use(fun next ->
                            //     (PipelineDelegate<MessagingContext> (fun ctx ct ->
                            //         let logger = ctx.Services.GetRequiredService<ILogger<MessageBus>>()
                            //         logger.LogInformation("Helooooooooooooooooooooooooo")
                            //         next.Invoke(ctx, ct))))
                            |> ignore)
                    |> ignore)
                |> ignore
        )
        |> ignore)

let c = buildContainer ()

let msgHost = c.GetRequiredService<IMessagingHost>()

let run () = msgHost.StartAsync() |> ignore

let stop () = msgHost.StopAsync().Wait()

let restart () =
    msgHost.ScheduleRestart(TimeSpan.FromSeconds 0)
