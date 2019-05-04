using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleAMQPWrapper;
using Xunit;

namespace Test {
    public class ReceiverTest {
        [Fact]
        public void ReceiveMainQueueTest() {
            ReceiverSubscribeCallback callback = (message) => {
                Console.WriteLine(string.Format("Message received: \"{0}\"", message));
                return true;
            };
            Factory.Receiver.subscribe(callback);

            Assert.Throws<DuplicateSubscriberException>(() => {
                Factory.Receiver.subscribe(callback);
            });
        }

        [Fact]
        public void ReceiveCustomQueueTest() {
            ReceiverSubscribeCallback callback = (message) => {
                Console.WriteLine(string.Format("Callback Message received: \"{0}\"", message));
                return true;
            };
            Factory.GetReceiverCustomQueue("customQueue1").subscribe(callback);

            Assert.Throws<DuplicateSubscriberException>(() => {
                Factory.GetReceiverCustomQueue("customQueue1").subscribe(callback);
            });

            Factory.GetReceiverCustomQueue("customQueue2").subscribe(callback);
        }

        [Fact]
        public void ReceiveCustomExchangeTest() {
            ReceiverSubscribeCallback callback = (message) => {
                Console.WriteLine(string.Format("Callback Message received: \"{0}\"", message));
                return true;
            };
            Factory.GetReceiverCustomExchange("customExchange1").subscribe(callback);

            Assert.Throws<DuplicateSubscriberException>(() => {
                Factory.GetReceiverCustomExchange("customExchange1").subscribe(callback);
            });

            Factory.GetReceiverCustomExchange("customExchange2").subscribe(callback);
        }

        [Fact]
        public void ReceiveStructureMainQueueTest() {
            ReceiverSubscribeCallback callback = (message) => {
                Console.WriteLine(string.Format("Message received: \"{0}\"", message));
                JObject messageObj = (JObject)JsonConvert.DeserializeObject(message);
                string action = messageObj.GetValue("Action").ToObject<string>();
                var messageConcrete = messageObj.GetValue("MessageData").ToObject<MessageDataConcrete>();
                return false;
            };
            Factory.Receiver.subscribe(callback);
        }
    }
}