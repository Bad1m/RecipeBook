namespace RecipeMicroservice.Domain.Constants
{
    public static class RabbitMqConfig
    {
        public const string HostName = "amqp://quest:quest@rabbitmq:5672";
        public const string ExchangeName = "recipe_exchange";
        public const string Key = "recipe";
        public const string DeleteQueue = "recipe_deleted_queue";
    }
}