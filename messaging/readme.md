# NBB.Messaging-interactive
A set of interactive scripts and jupyter notebooks for playing with NBB messaging apis



## Message Bus
Open in fsharp interactive
```shell
cd .\messaging\
dotnet fsi
#load "MsgBus.fsx";;
open MsgBus;;
```
Examples:

- publish message to topic `MyTopic`
    ```fsharp
    pub {| id = 1 |} "MyTopic" "tenantId" |> wait;;
    ```
- subscribe to topic `MyTopic`
    ```fsharp
    let s = sub doNothing "MyTopic" |> wait;;
    ```

## Messaging Host
Open in fsharp interactive
```shell
cd .\messaging\
dotnet fsi
#load "MsgHost.fsx";;
```
Examples:

- start the messaging host 
    ```fsharp
    MsgHost.run ();;
    ```
- stop the messaging host
    ```fsharp
    MsgHost.stop ();;
    ```
- restart the messaging host
    ```fsharp
    MsgHost.restart ();;
    ```


