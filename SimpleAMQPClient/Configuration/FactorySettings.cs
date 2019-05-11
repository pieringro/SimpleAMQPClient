using System.IO;
using Microsoft.Extensions.Configuration;

namespace SimpleAMQPWrapper.Configuration {
    public class FactorySettings : Settings {

        private static FactorySettings _instance;
        public static FactorySettings Instance {
            get {
                if (_instance == null || _instance.refreshInstance) {
                    _instance = new FactorySettings();
                    _instance.buildConfigurations("SimpleAMQPWrapper.FactorySettings");
                }
                return _instance;
            }
        }

        private FactorySettings() {

        }

        private string _AMQP;
        public string AMQP {
            get {
                if (_AMQP == null) {
                    _AMQP = ConfigurationSection["AMQP"];
                }
                return _AMQP;
            }
        }

        private string _SenderAMQPClassName;
        public string SenderAMQPClassName {
            get {
                if (_SenderAMQPClassName == null) {
                    _SenderAMQPClassName = ConfigurationSection.GetSection(AMQP)["SenderAMQP"];
                }
                return _SenderAMQPClassName;
            }
        }

        private string _ReceiverAMQPClassName;
        public string ReceiverAMQPClassName {
            get {
                if (_ReceiverAMQPClassName == null) {
                    _ReceiverAMQPClassName = ConfigurationSection.GetSection(AMQP)["ReceiverAMQP"];
                }
                return _ReceiverAMQPClassName;
            }
        }
    }
}