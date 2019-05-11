namespace SimpleAMQPWrapper {

    public abstract class ISender {
        public string hostname { get; set; }
        public string queue { get; set; }
        public string exchangeFanout { get; set; }
        public abstract void init();
        public abstract void publishMessage(string message);
        public abstract void publishStructureMessage(string action, IMessageData obj);
    }
}