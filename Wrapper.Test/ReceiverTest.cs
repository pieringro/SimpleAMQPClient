using System;
using SimpleAMQPWrapper;
using Xunit;

namespace Test {
    public class ReceiverTest {
        [Fact]
        public void ReceiveMessageTest() {
            ReceiverSubscribeCallback callback = (message) => {
                Console.WriteLine(string.Format("Message received: \"{0}\"", message));
                return true;
            };
            Factory.Receiver.subscribe(callback);
        }
    }
}