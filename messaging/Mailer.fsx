#r "nuget: NBB.Messaging.Nats, 6.0.22"
#r "nuget: NBB.Messaging.Rusi, 6.0.22"
#r "nuget: Microsoft.Extensions.DependencyInjection, 6.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 6.0.1"
#r "nuget: Microsoft.Extensions.Configuration, 6.0.0"
#r "nuget: Microsoft.Extensions.Configuration.Json, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging.Console, 6.0.0"

#load "MsgBus.fsx"
open System

let events = [ "ch.events.Mailer.PublishedLanguage.Events.EmailSent" ]
let subs: IDisposable list = []

let subAll () =
    subs = (events |> List.map MsgBus.sub)

let unsubAll () =
    subs |> List.map (fun s -> s.Dispose())

let sendMail tenantId =
    let topic = "ch.commands.Mailer.PublishedLanguage.Commands.SendEmail"

    let msg =
        {| Subject = "Hello"
           Body = "World"
           To =
            [ {| Address = "rpopovici@totalsoft.ro"
                 DisplayName = "rpopovici" |} ] |}

    MsgBus.pubT topic msg tenantId
