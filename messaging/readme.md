# NBB.Messaging-interactive
A set of interactive scripts and jupyter notebooks for playing with NBB messaging apis



## Message Bus
Open in fsharp interactive
```shell
cd .\messaging\
dotnet fsi
#load "MsgBus.fsx";;
```
Examples:

- publish 10 messages to topic `MyTopic`
    ```fsharp
    MsgBus.pub "MyTopic" 10;;
    ```
- subscribe to topic `MyTopic`
    ```fsharp
    let s = MsgBus.sub "MyTopic";;
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


