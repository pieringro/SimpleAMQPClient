# SimpleAMQPClient
.Net Core Class library. Client for Advance Message Queuing Protocol. RabbitMQ support.

**Get started (with Docker)**

 * Start docker.
 * On Windows: double click to execute.bat
 * Or execute commands:
```sh
docker build -t my_rabbitmq .
docker run -d -p 5672:5672 -p 15672:15672 my_rabbitmq
```

Now RabbitMQ is ready with plugin rabbitmq_management.

**Get started**

Just install RabbitMQ on your machine and listen on default port 5672.

**Snippets**

**How to send a message in a queue**

```c
Factory.Sender.publishMessage("hello world!");
```

**How to be a consumer**

```c
Factory.Receiver.subscribe(
    (message) => {
        Console.WriteLine(string.Format("Message received {0}", message));
        return true;
    }
);
```

Queues names for Sender and for Receiver are in appsettings.json configuration along with the host name.
```json
"RabbitMQSettings": {
    "Hostname": "localhost",
    "QueueReceiver": "hello",
    "QueueSender": "hello"
}
```

**Send message and be consumer to custom queues**

```c
Factory.GetSenderCustom("customQueue").publishMessage("hello world!");
```

```c
Factory.GetReceiverCustom("customQueue").subscribe(
    (message) => {
        Console.WriteLine(string.Format("Message received {0}", message));
        return true;
    }
);
```


**Dependencies**

```sh
dotnet restore
```

* RabbitMQ.Client Version 5.1.0



