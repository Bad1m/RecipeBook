namespace ReviewMicroservice.Application.Interfaces
{
    public interface IRabbitMqConsumer
    {
        void StartConsuming();
        void Dispose();
    }
}