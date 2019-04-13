using System.IO;
using Microsoft.Extensions.Configuration;

namespace RabbitMQ.Configuration {
    internal class RabbitMQSettings : Settings {

        private static RabbitMQSettings _instance;
        public static RabbitMQSettings Instance {
            get {
                if (_instance == null || _instance.refreshInstance) {
                    _instance = new RabbitMQSettings();
                    _instance.buildConfigurations("RabbitMQSettings");
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
                    _HostName = ConfigurationSection["hostname"];
                }
                return _HostName;
            }
        }

        private string _Queue;
        public string Queue {
            get {
                if (_Queue == null) {
                    _Queue = ConfigurationSection["queue"];
                }
                return _Queue;
            }
        }

    }
}