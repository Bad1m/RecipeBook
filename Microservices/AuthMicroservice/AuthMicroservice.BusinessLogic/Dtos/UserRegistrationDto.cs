namespace AuthMicroservice.BusinessLogic.Dtos
{
    public class UserRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? UserName { get; init; }
        public string? Password { get; init; }
        public string? Email { get; init; }
    }
}