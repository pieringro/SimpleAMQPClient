using System;
using System.Collections.Generic;
using System.Reflection;
using SimpleAMQPWrapper.Configuration;

namespace SimpleAMQPWrapper {

    public static class Factory {

        #region Receiver
        private static IReceiver _receiver;
        public static IReceiver Receiver {
            get {
                try {
                    if (_receiver == null) {
                        _receiver = buildReceiver();
                    }
                } catch (Exception e) {
                    throw new Exception("Exception during get Receiver. " + e.Message);
                }
                return _receiver;
            }
        }

        private static Dictionary<string, IReceiver> _receiversMap = new Dictionary<string, IReceiver>();
        public static IReceiver GetReceiverCustom(string queue) {
            IReceiver receiver = null;
            if (_receiversMap.ContainsKey(queue)) {
                receiver = _receiversMap[queue];
            } else {
                receiver = buildReceiver(queue);
                _receiversMap.Add(queue, receiver);
            }
            return receiver;
        }

        private static IReceiver buildReceiver(params string[] args) {
            IReceiver receiver;

            string className = FactorySettings.Instance.ReceiverAMQPClassName;
            if (className == null) {
                throw new Exception("Unable to get Receiver configurated.");
            }
            Type t = Type.GetType(className);
            if (t == null) {
                throw new Exception(string.Format("Receiver configuration error. {0} not found", className));
            }
            receiver = (IReceiver)Activator.CreateInstance(t, args);

            return receiver;
        }
        #endregion

        #region Sender

        private static ISender _sender;
        public static ISender Sender {
            get {
                try {
                    if (_sender == null) {
                        _sender = buildSender();
                    }
                } catch (Exception e) {
                    throw new Exception("Exception during get Sender. " + e.Message);
                }
                return _sender;
            }
        }

        private static Dictionary<string, ISender> _sendersMap = new Dictionary<string, ISender>();
        public static ISender GetSenderCustom(string queue) {
            ISender sender = null;
            if (_sendersMap.ContainsKey(queue)) {
                sender = _sendersMap[queue];
            } else {
                sender = buildSender(queue);
                _sendersMap.Add(queue, sender);
            }
            return sender;
        }

        private static ISender buildSender(params string[] args) {
            ISender sender;

            string className = FactorySettings.Instance.SenderAMQPClassName;
            if (className == null) {
                throw new Exception("Unable to get Sender configurated.");
            }
            Type t = Type.GetType(className);
            if (t == null) {
                throw new Exception(string.Format("Sender configuration error. {0} not found", className));
            }
            sender = (ISender)Activator.CreateInstance(t, args);

            return sender;
        }
        #endregion
    }
}