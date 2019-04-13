using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Configuration;

namespace RabbitMQ {
    internal class RabbitReceiver : IReceiver {
        public string hostname { get; private set; }
        public string queue { get; private set; }
        private IModel channel;
        public RabbitReceiver() {
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

        public void subscribe(ReceiverSubscribeCallback callback) {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) => {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                callback(message);
            };
            channel.BasicConsume(queue: this.queue,
                autoAck: true,
                consumer: consumer);
        }
    }
}