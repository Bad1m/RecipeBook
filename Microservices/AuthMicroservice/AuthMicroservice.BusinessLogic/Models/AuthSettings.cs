namespace AuthMicroservice.BusinessLogic.Models
{
    public class AuthSettings
    {
        public string Secret { get; set; }
        public TimeSpan ExpiresIn { get; set; }
        public TimeSpan RefreshTokenValidityInDays { get; set; }
    }
}