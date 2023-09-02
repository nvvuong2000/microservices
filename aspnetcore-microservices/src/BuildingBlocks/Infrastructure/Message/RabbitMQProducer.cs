using Constracts.Message;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Message
{
    public class RabbitMQProducer : IMessageProducer
    {
        public RabbitMQProducer()
        {

        }
        public void SendMessage<T>(T messgae)
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("orders", exclusive: false);

            var jsonData = JsonConvert.SerializeObject(messgae);
            var body = Encoding.UTF8.GetBytes(jsonData);
            
            channel.BasicPublish(exchange:"",routingKey: "orders", body:body);
        }
    }
}
