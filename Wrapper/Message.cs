using System;
using System.Runtime.Serialization;

namespace SimpleAMQPWrapper {
    public class Message {
        public string Action;
        public IMessageData MessageData;
    }
}