using System;
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
            Factory.GetReceiverCustom("customQueue1").subscribe(callback);

            Assert.Throws<DuplicateSubscriberException>(() => {
                Factory.GetReceiverCustom("customQueue1").subscribe(callback);
            });

            Factory.GetReceiverCustom("customQueue2").subscribe(callback);

        }
    }
}