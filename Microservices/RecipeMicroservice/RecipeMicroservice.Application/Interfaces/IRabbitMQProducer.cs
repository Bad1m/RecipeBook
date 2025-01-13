namespace RecipeMicroservice.Application.Interfaces
{
    public interface IRabbitMqProducer
    {
        public void SendMessage<T>(T message);
        void Dispose();
    }
}