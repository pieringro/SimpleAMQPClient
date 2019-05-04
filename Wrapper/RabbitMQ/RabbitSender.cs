using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using SimpleAMQPWrapper.RabbitMQ.Configuration;

namespace SimpleAMQPWrapper.RabbitMQ {
    internal class RabbitSender : ISender {

        private IModel channel;

        public override void init() {
            this.setConfiguration();

            var factory = new ConnectionFactory() { HostName = this.hostname };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            if (!string.IsNullOrEmpty(this.exchangeFanout)) {
                this.initForExchangeFanout();
            } else if (!string.IsNullOrEmpty(this.queue)) {
                this.initForQueue();
            }
        }

        private void setConfiguration() {
            try {
                if (queue == null) {
                    queue = RabbitMQSettings.Instance.QueueReceiver;
                }
                if (exchangeFanout == null) {
                    exchangeFanout = RabbitMQSettings.Instance.ExchangeFanoutReceiver;
                }
                hostname = RabbitMQSettings.Instance.HostName;
            } catch (Exception e) {
                throw new Exception("Unable to start configuration for RabbitSender: " + e.Message);
            }
        }

        private void initForExchangeFanout() {
            channel.ExchangeDeclare(exchange: this.exchangeFanout,
                type: "fanout");
            this.queue = "";
        }

        private void initForQueue() {
            channel.QueueDeclare(queue: this.queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public override void publishMessage(string message) {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: this.exchangeFanout,
                routingKey: this.queue,
                basicProperties: null,
                body: body);
        }

        public override void publishStructureMessage(string action, IMessageData data) {
            var message = new Message() {
                Action = action,
                MessageData = data
            };
            var body = Encoding.UTF8.GetBytes(serializeMessage(message));
            channel.BasicPublish(exchange: this.exchangeFanout,
                routingKey : this.queue,
                basicProperties : null,
                body : body);
        }

        private string serializeMessage(Message message) {
            string json = JsonConvert.SerializeObject(message);
            return json;
        }
    }
}