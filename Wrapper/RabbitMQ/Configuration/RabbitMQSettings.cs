using System.IO;
using Microsoft.Extensions.Configuration;

namespace SimpleAMQPWrapper.RabbitMQ.Configuration {
    internal class RabbitMQSettings : Settings {

        private static RabbitMQSettings _instance;
        public static RabbitMQSettings Instance {
            get {
                if (_instance == null || _instance.refreshInstance) {
                    _instance = new RabbitMQSettings();
                    _instance.buildConfigurations("SimpleAMQPWrapper.RabbitMQSettings");
                }
                return _instance;
            }
        }

        private RabbitMQSettings() {

        }

        private string _HostName;
        public string HostName {
            get {
                if (_HostName == null) {
                    _HostName = ConfigurationSection["Hostname"];
                }
                return _HostName;
            }
        }

        private string _QueueSender;
        public string QueueSender {
            get {
                if (_QueueSender == null) {
                    _QueueSender = ConfigurationSection["QueueSender"];
                }
                return _QueueSender;
            }
        }

        private string _QueueReceiver;
        public string QueueReceiver {
            get {
                if (_QueueReceiver == null) {
                    _QueueReceiver = ConfigurationSection["QueueReceiver"];
                }
                return _QueueReceiver;
            }
        }

    }
}