using System;
using System.Reflection;
using Configuration;

public static class Factory {

    private static IReceiver _receiver;
    public static IReceiver Receiver {
        get {
            try {

                if (_receiver == null) {
                    string className = FactorySettings.Instance.ReceiverAMQP;
                    if (className == null) {
                        throw new Exception("Unable to get Receiver configurated.");
                    }
                    Type t = Type.GetType(className);
                    _receiver = (IReceiver)Activator.CreateInstance(t);
                }
            } catch (Exception e) {
                throw new Exception("Exception during get Receiver. " + e.Message);
            }
            return _receiver;
        }
    }

    private static ISender _sender;
    public static ISender Sender {
        get {
            try {

                if (_sender == null) {
                    string className = FactorySettings.Instance.SenderAMQP;
                    if (className == null) {
                        throw new Exception("Unable to get Receiver configurated.");
                    }
                    Type t = Type.GetType(className);
                    _sender = (ISender)Activator.CreateInstance(t);
                }
            } catch (Exception e) {
                throw new Exception("Exception during get Receiver. " + e.Message);
            }
            return _sender;
        }
    }
}