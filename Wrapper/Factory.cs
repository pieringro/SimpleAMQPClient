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
                        _receiver.init();
                    }
                } catch (Exception e) {
                    throw new Exception("Exception during get Receiver. " + e.Message);
                }
                return _receiver;
            }
        }

        private static Dictionary<string, IReceiver> _receiversQueueMap = new Dictionary<string, IReceiver>();
        private static Dictionary<string, IReceiver> _receiversExchangeMap = new Dictionary<string, IReceiver>();
        public static IReceiver GetReceiverCustomQueue(string queue) {
            IReceiver receiver = createReceiverIfNotExistInMap(queue, _receiversQueueMap);
            if (!receiver.listening) {
                receiver.queue = queue;
                receiver.exchangeFanout = "";
                receiver.init();
            }
            return receiver;
        }
        public static IReceiver GetReceiverCustomExchange(string exchange) {
            IReceiver receiver = createReceiverIfNotExistInMap(exchange, _receiversExchangeMap);
            if (!receiver.listening) {
                receiver.exchangeFanout = exchange;
                receiver.queue = "";
                receiver.init();
            }
            return receiver;
        }

        private static IReceiver createReceiverIfNotExistInMap(string key, Dictionary<string, IReceiver> map) {
            IReceiver receiver = null;
            if (map.ContainsKey(key)) {
                receiver = map[key];
            } else {
                receiver = buildReceiver();
                map.Add(key, receiver);
            }
            return receiver;
        }

        private static IReceiver buildReceiver() {
            IReceiver receiver;

            string className = FactorySettings.Instance.ReceiverAMQPClassName;
            if (className == null) {
                throw new Exception("Unable to get Receiver configurated.");
            }
            Type t = Type.GetType(className);
            if (t == null) {
                throw new Exception(string.Format("Receiver configuration error. {0} not found", className));
            }
            receiver = (IReceiver)Activator.CreateInstance(t);

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
                        _sender.init();
                    }
                } catch (Exception e) {
                    throw new Exception("Exception during get Sender. " + e.Message);
                }
                return _sender;
            }
        }

        private static Dictionary<string, ISender> _sendersQueueMap = new Dictionary<string, ISender>();
        private static Dictionary<string, ISender> _sendersExchangeMap = new Dictionary<string, ISender>();

        public static ISender GetSenderCustomQueue(string queue) {
            ISender sender = null;
            bool exists = createSenderIfNotExistInMap(queue, _sendersQueueMap, out sender);
            if (!exists) {
                sender.queue = queue;
                sender.exchangeFanout = "";
                sender.init();
            }
            return sender;
        }
        public static ISender GetSenderCustomExchange(string exchange) {
            ISender sender = null;
            bool exists = createSenderIfNotExistInMap(exchange, _sendersExchangeMap, out sender);
            if (!exists) {
                sender.exchangeFanout = exchange;
                sender.queue = "";
                sender.init();
            }
            return sender;
        }

        private static bool createSenderIfNotExistInMap(string key, Dictionary<string, ISender> map, out ISender sender) {
            sender = null;
            if (map.ContainsKey(key)) {
                sender = map[key];
                return true;
            } else {
                sender = buildSender();
                map.Add(key, sender);
                return false;
            }
        }

        private static ISender buildSender() {
            ISender sender;

            string className = FactorySettings.Instance.SenderAMQPClassName;
            if (className == null) {
                throw new Exception("Unable to get Sender configurated.");
            }
            Type t = Type.GetType(className);
            if (t == null) {
                throw new Exception(string.Format("Sender configuration error. {0} not found", className));
            }
            sender = (ISender)Activator.CreateInstance(t);

            return sender;
        }
        #endregion
    }
}