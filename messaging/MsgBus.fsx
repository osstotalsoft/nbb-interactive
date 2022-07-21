#r "nuget: NBB.Messaging.Nats, 6.0.19"
#r "nuget: NBB.Messaging.Rusi, 6.0.19"
#r "nuget: Microsoft.Extensions.DependencyInjection, 6.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 6.0.1"
#r "nuget: Microsoft.Extensions.Configuration, 6.0.0"
#r "nuget: Microsoft.Extensions.Configuration.Json, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging.Console, 6.0.0"

#load "CompositionRoot.fsx"

open Microsoft.Extensions.DependencyInjection
open NBB.Messaging.Abstractions
open System.Threading.Tasks

let c = CompositionRoot.buildContainer (fun _ _ -> ())

let pubAsync topic cnt =
    let msgBus = c.GetRequiredService<IMessageBusPublisher>()
    let o = MessagingPublisherOptions.Default
    o.TopicName <- topic

    task {
        for i in 1..cnt do
            do! msgBus.PublishAsync({| id = i |}, o)
    }

let pub topic cnt = 
    pubAsync topic cnt |> ignore


let subAsync (h: MessagingEnvelope -> Task<unit>) topic =
    let msgBus = c.GetRequiredService<IMessageBusSubscriber>()
    let o = MessagingSubscriberOptions.Default
    o.TopicName <- topic
    task {
        let h1 e = h(e) :> Task
        return! msgBus.SubscribeAsync(h1, o)
    }

let sub topic = (subAsync (fun _e -> task {return ()}) topic).Result

let infiniteSub topic = 
    let h _e = pubAsync topic 1
    let t = subAsync h topic
    t.Result

let subMany cnt = 
    [1..cnt] 
    |> List.map (sprintf "topic_%i")
    |> List.map sub
