using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReviewMicroservice.Application.Messages;
using System.Text;
using Newtonsoft.Json;
using ReviewMicroservice.Infrastructure.Interfaces;
using ReviewMicroservice.Domain.Settings;
using ReviewMicroservice.Application.Interfaces;

namespace ReviewMicroservice.Application.Consumers
{
    public class RabbitMqConsumer : IRabbitMqConsumer, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly string _routingKey;
        private readonly IReviewRepository _reviewRepository;

        public RabbitMqConsumer(
            string rabbitMqConnectionString,
            string exchangeName,
            string queueName,
            string routingKey,
            IReviewRepository reviewRepository)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqConnectionString)
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _exchangeName = exchangeName;
            _queueName = queueName;
            _routingKey = routingKey;
            _reviewRepository = reviewRepository;
            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Direct);
            _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _routingKey);
        }

        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var recipeDeletedMessage = JsonConvert.DeserializeObject<RecipeDeletedMessage>(message);
                await ProcessMessageAsync(recipeDeletedMessage);
                _channel.BasicAck(ea.DeliveryTag, multiple: false);
            };
            _channel.BasicConsume(queue: _queueName, autoAck: false, consumer: consumer);
        }

        private async Task ProcessMessageAsync(RecipeDeletedMessage recipeDeletedMessage)
        {
            var reviewsExist = await _reviewRepository.GetByRecipeIdAsync(recipeDeletedMessage.RecipeId, new PaginationSettings { }, CancellationToken.None);

            if (reviewsExist.Count != 0)
            {
                await _reviewRepository.DeleteReviewsByRecipeIdAsync(recipeDeletedMessage.RecipeId, CancellationToken.None);
            }
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}