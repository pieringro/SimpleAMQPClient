namespace SimpleAMQPWrapper {

    public interface IReceiver {
        void subscribe(ReceiverSubscribeCallback callback);
    }
}