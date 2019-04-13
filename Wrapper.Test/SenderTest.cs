using System;
using Xunit;

namespace Test {
    public class SenderTest {
        [Fact]
        public void PublishMessageTest() {
            Factory.Sender.publishMessage("unit test hello");
        }
    }
}