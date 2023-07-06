#r "nuget: NBB.Messaging.Nats, 6.0.22"
#r "nuget: NBB.Messaging.Rusi, 6.0.22"
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

let pub msg topic tenantId =
    let msgBus = c.GetRequiredService<IMessageBusPublisher>()

    let o =
        new MessagingPublisherOptions(
            TopicName = topic,
            EnvelopeCustomizer = fun envelope -> envelope.SetHeader("nbb-tenantId", tenantId)
        )

    task { do! msgBus.PublishAsync(msg, o) }

let sub (h: MessagingEnvelope -> Task<unit>) topic =
    let msgBus = c.GetRequiredService<IMessageBusSubscriber>()

    let o =
        new MessagingSubscriberOptions(TopicName = topic, Transport = SubscriptionTransportOptions.RequestReply)

    task {
        let h1 e = h (e) :> Task
        return! msgBus.SubscribeAsync(h1, o)
    }

let infiniteSub msg topic tenantId =
    let h (envelope: MessagingEnvelope) = pub envelope.Payload topic tenantId

    task {
        let! s = sub h topic
        do! pub msg topic tenantId
        return s
    }

let wait (t: Task<'a>) = t.GetAwaiter().GetResult()

let doNothing _ = Task.FromResult()