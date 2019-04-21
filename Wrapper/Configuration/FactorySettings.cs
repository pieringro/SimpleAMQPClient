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

        private string _SenderAMQP;
        public string SenderAMQP {
            get {
                if (_SenderAMQP == null) {
                    _SenderAMQP = ConfigurationSection.GetSection(AMQP)["SenderAMQP"];
                }
                return _SenderAMQP;
            }
        }

        private string _ReceiverAMQP;
        public string ReceiverAMQP {
            get {
                if (_ReceiverAMQP == null) {
                    _ReceiverAMQP = ConfigurationSection.GetSection(AMQP)["ReceiverAMQP"];
                }
                return _ReceiverAMQP;
            }
        }
    }
}