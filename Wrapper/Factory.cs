using System;
using System.Reflection;
using Configuration;

public static class Factory {

    private static IReceiver _receiver;
    public static IReceiver Receiver {
        get {
            if (_receiver == null) {
                string className = FactorySettings.Instance.ReceiverAMQP;
                Type t = Type.GetType(className);
                _receiver = (IReceiver) Activator.CreateInstance(t);
            }
            return _receiver;
        }
    }

    private static ISender _sender;
    public static ISender Sender {
        get {
            if (_sender == null) {
                string className = FactorySettings.Instance.SenderAMQP;
                Type t = Type.GetType(className);
                _sender = (ISender) Activator.CreateInstance(t);
            }
            return _sender;
        }
    }
}