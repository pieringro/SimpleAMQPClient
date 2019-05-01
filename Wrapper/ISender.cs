namespace SimpleAMQPWrapper {

    public interface ISender {
        void publishMessage(string message);
        void publishStructureMessage(string action, IMessageData obj);
    }
}