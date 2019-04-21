namespace SimpleAMQPWrapper {

    public interface ISender {
        void publishMessage(string message);
    }
}