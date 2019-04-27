using System;
using SimpleAMQPWrapper;
using Xunit;

namespace Test {
    public class SenderTest {
        [Fact]
        public void PublishMainQueueTest() {
            Factory.Sender.publishMessage("unit test hello");
        }

        [Fact]
        public void ReceiveCustomQueueTest() {
            
            Factory.GetSenderCustom("customQueue1").publishMessage("queue1 unit test hello");

            Factory.GetSenderCustom("customQueue2").publishMessage("queue2 unit test hello");

        }
    }
}