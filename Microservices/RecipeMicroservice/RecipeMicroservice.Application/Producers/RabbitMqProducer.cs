using Newtonsoft.Json;
using RabbitMQ.Client;
using RecipeMicroservice.Application.Interfaces;
using System.Text;

namespace RecipeMicroservice.Application.Producers
{
    public class RabbitMqProducer : IRabbitMqProducer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _routingKey;

        public RabbitMqProducer(string rabbitMqConnectionString, string exchangeName, string routingKey)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqConnectionString)
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;
            _routingKey = routingKey;
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
        }

        public void SendMessage<T>(T message)
        {
            var jsonMessage = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            _channel.BasicPublish(_exchangeName, _routingKey, null, body);
        }

        public void Dispose()
        {
            _channel.Close();
            _connection.Close();
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}