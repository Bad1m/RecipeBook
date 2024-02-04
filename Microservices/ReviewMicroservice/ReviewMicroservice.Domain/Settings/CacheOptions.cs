namespace ReviewMicroservice.Domain.Settings
{
    public class CacheOptions
    {
        public TimeSpan AbsoluteExpiration { get; set; } = TimeSpan.FromSeconds(300);
        public TimeSpan SlidingExpiration { get; set; } = TimeSpan.FromSeconds(300);
    }
}