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

Queues names and Exchanges fanout names for Sender and for Receiver are in appsettings.json configuration along with the host name.
```json
"RabbitMQSettings": {
    "Hostname": "localhost",
    "QueueReceiver": "hello",
    "QueueSender": "hello",
    "ExchangeFanoutReceiver": "hello_exchange",
    "ExchangeFanoutSender": "hello_exchange"
}
```

"ExchangeFanoutReceiver" and "ExchangeFanoutSender" have priority.
If Queue and Exchange are set, main messages in Sender are sent to Exchange (queue is ignored). The same for Receiver.


**Send message and be consumer to custom queues**

```c
Factory.GetSenderCustomQueue("customQueue").publishMessage("hello world!");
```

```c
Factory.GetReceiverCustomQueue("customQueue").subscribe(
    (message) => {
        Console.WriteLine(string.Format("Message received {0}", message));
        return true;
    }
);
```

In those cases, it uses the queue customQueue. Configurations are ignored.


**Send message and be consumer to custom exchanges**

```c
Factory.GetSenderCustomExchange("customExchange").publishMessage("queue1 unit test hello");
```

```c
Factory.GetReceiverCustomExchange("customExchange").subscribe(
    (message) => {
        Console.WriteLine(string.Format("Message received: \"{0}\"", message));
        return true;
    }
);
```

**Send and be consumer of structure messages.**

From the unit test:

```c
var message = new MessageDataConcrete() {
    Prop0 = "unit test property one",
    Prop1 = "unit test property two"
};
Factory.Sender.publishStructureMessage("unit test action!", message);
```

```c
ReceiverSubscribeCallback callback = (message) => {
    Console.WriteLine(string.Format("Message received: \"{0}\"", message));
    JObject messageObj = (JObject)JsonConvert.DeserializeObject(message);
    string action = messageObj.GetValue("Action").ToObject<string>();
    var messageConcrete = messageObj.GetValue("MessageData").ToObject<MessageDataConcrete>();
    return false;
};
Factory.Receiver.subscribe(callback);
```


**Dependencies**

```sh
dotnet restore
```

* RabbitMQ.Client Version 5.1.0
* Microsoft.Extensions.Configuration.Json Version 2.2.0


