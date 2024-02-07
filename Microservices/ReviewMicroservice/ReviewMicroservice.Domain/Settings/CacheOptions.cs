namespace ReviewMicroservice.Domain.Settings
{
    public class CacheOptions
    {
        public TimeSpan AbsoluteExpiration { get; set; }
        public TimeSpan SlidingExpiration { get; set; }
    }
}