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


let processInvoiceTopic = "NBB.Invoices.PublishedLanguage.ProcessInvoice"

let processInvoice cnt = MsgBus.pub {| InvoiceId = "0FB78DEC-B01E-492F-B659-A749F7AF4F35" |} processInvoiceTopic ""