namespace SimpleAMQPWrapper {

    public abstract class IReceiver {
        public string hostname { get; protected set; }
        public string queue { get; set; }
        public string exchangeFanout { get; set; }
        public bool listening { get; protected set; } = false;
        public abstract void init();
        public abstract void subscribe(ReceiverSubscribeCallback callback);
    }
}