using System;
using Xunit;
using SimpleAMQPWrapper;

namespace Test {
    public class SenderTest {
        [Fact]
        public void PublishMessageTest() {
            Factory.Sender.publishMessage("unit test hello");
        }
    }
}