using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimpleAMQPWrapper.RabbitMQ.Configuration;

namespace SimpleAMQPWrapper.RabbitMQ {
    internal class RabbitReceiver : IReceiver {
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
                throw new Exception("Unable to start configuration for RabbitReceiver: " + e.Message);
            }
        }

        private void initForQueue() {
            channel.QueueDeclare(queue: this.queue,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        private void initForExchangeFanout() {
            channel.ExchangeDeclare(exchange: this.exchangeFanout, type: "fanout");

            //create a new queue on this exchange
            this.queue = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: this.queue,
                exchange: this.exchangeFanout,
                routingKey: "");
        }

        public override void subscribe(ReceiverSubscribeCallback callback) {
            if (!listening) {
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) => {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var result = callback(message);
                    if (result) {
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                };
                channel.BasicConsume(queue: this.queue,
                    autoAck: false,
                    consumer: consumer);
                listening = true;
            } else {
                throw new DuplicateSubscriberException();
            }
        }
    }
}