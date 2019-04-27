﻿using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SimpleAMQPWrapper.RabbitMQ.Configuration;

namespace SimpleAMQPWrapper.RabbitMQ {
    internal class RabbitReceiver : IReceiver {
        public string hostname { get; private set; }
        public string queue { get; private set; }
        private IModel channel;
        private bool listening = false;
        public RabbitReceiver() {
            this.setConfiguration();
            this.init();
        }

        public RabbitReceiver(string queue) {
            this.queue = queue;
            this.setConfiguration();
            this.init();
        }

        private void setConfiguration() {
            try {
                if (queue == null) {
                    queue = RabbitMQSettings.Instance.QueueReceiver;
                }
                hostname = RabbitMQSettings.Instance.HostName;
            } catch (Exception e) {
                throw new Exception("Unable to start configuration for RabbitReceiver: " + e.Message);
            }
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