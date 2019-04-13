using System;
using System.Text;
using RabbitMQ.Configuration;
using RabbitMQ.Client;

namespace RabbitMQ {
    internal class RabbitSender : ISender {
        public string hostname { get; private set; }
        public string queue { get; private set; }
        private IModel channel;
        public RabbitSender() {
            this.setConfiguration();
            this.init();
        }

        private void setConfiguration() {
            queue = RabbitMQSettings.Instance.Queue;
            hostname = RabbitMQSettings.Instance.HostName;
        }

        private void init() {
            var factory = new ConnectionFactory() { HostName = this.hostname };
            var connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.QueueDeclare(queue: this.queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void publishMessage(string message) {
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                routingKey : this.queue,
                basicProperties : null,
                body : body);
        }
    }
}