namespace RecipeMicroservice.Domain.Settings
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; }
        public string ExchangeName { get; set; }
        public string Key { get; set; }
        public string DeleteQueue { get; set; }
    }
}