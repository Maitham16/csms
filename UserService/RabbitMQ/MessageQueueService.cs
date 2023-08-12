using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace UserService.RabbitMQ
{
    public class MessageQueueService
    {
        private readonly string _hostname;
        private readonly string _queueName;

        public MessageQueueService(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
        }

        public void SendMessage<T>(T message)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            channel.BasicPublish(exchange: "",
                                 routingKey: _queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
