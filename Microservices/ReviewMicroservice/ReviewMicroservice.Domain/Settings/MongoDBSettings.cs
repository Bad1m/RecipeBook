namespace ReviewMicroservice.Domain.Settings
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; }
        public string DockerConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}