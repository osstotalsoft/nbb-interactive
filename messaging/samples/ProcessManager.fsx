#r "nuget: NBB.Messaging.Nats, 6.0.22"
#r "nuget: NBB.Messaging.Rusi, 6.0.22"
#r "nuget: Microsoft.Extensions.DependencyInjection, 6.0.0"
#r "nuget: Microsoft.Extensions.Hosting, 6.0.1"
#r "nuget: Microsoft.Extensions.Configuration, 6.0.0"
#r "nuget: Microsoft.Extensions.Configuration.Json, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging, 6.0.0"
#r "nuget: Microsoft.Extensions.Logging.Console, 6.0.0"

#load "../MsgBus.fsx"

open MsgBus

let mailerProvisionedSuccessfully =
    {| TenantId = System.Guid("5efa942b-4d37-407a-bf9b-03f675d116b2")
       TenantName = "RADU"
       TenantDescription = "Radu"
       Platform = "QA"
       Domain = "Mailer" |}

do
    pub mailerProvisionedSuccessfully "PlatformControllers.ProvisioningController.TenantProvisionedSuccessfully" ""
    |> wait

let tasksProvisionedSuccessfully =
    {| TenantId = System.Guid("5efa942b-4d37-407a-bf9b-03f675d116b2")
       TenantName = "RADU"
       TenantDescription = "Radu"
       Platform = "QA"
       Domain = "Tasks" |}

do
    pub tasksProvisionedSuccessfully "PlatformControllers.ProvisioningController.TenantProvisionedSuccessfully" ""
    |> wait

let mercuryProvisionedSuccessfully =
    {| TenantId = System.Guid("5efa942b-4d37-407a-bf9b-03f675d116b2")
       TenantName = "RADU"
       TenantDescription = "Radu"
       Platform = "QA"
       Domain = "Mercury" |}

do
    pub mercuryProvisionedSuccessfully "PlatformControllers.ProvisioningController.TenantProvisionedSuccessfully" ""
    |> wait

let mercuryConfiguredSuccessfully =
    {| TenantId = System.Guid("5efa942b-4d37-407a-bf9b-03f675d116b2")
       Code = "RADU" |}

do
    pub
        mercuryConfiguredSuccessfully
        "ch.events.Mercury.PublishedLanguage.Events.TenantConfiguredSuccessfully"
        ""
    |> wait

let tasksConfiguredSuccessfully =
    {| TenantId = System.Guid("5efa942b-4d37-407a-bf9b-03f675d116b2")
       Code = "RADU" |}

do
    pub
        tasksConfiguredSuccessfully
        "ch.commands.Tasks.PublishedLanguage.Events.MultiTenancy.TenantConfigurationSuccessfully"
        ""
    |> wait



let mercuryProcessEvent = {|  |}

do
    pub mercuryProcessEvent "ch.commands.Mercury.PublishedLanguage.Commands.ProcessEvent" ""
    |> wait

let s =
    sub (fun _ -> task { return () }) "ch.commands.Mercury.PublishedLanguage.Commands.ProcessEvent"



do
    pub {|  |} "ch.events.Tasks.Definition.Application.Events.EventDefinitionUpdated" ""
    |> wait
