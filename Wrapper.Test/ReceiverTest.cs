using System;
using Xunit;

namespace Test {
    public class ReceiverTest {
        [Fact]
        public void ReceiveMessageTest() {
            Factory.Receiver.subscribe(
                (message) => {
                    Console.WriteLine(string.Format("Message received {0}", message));
                }
            );
        }
    }
}