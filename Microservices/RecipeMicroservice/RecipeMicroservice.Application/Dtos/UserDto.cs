namespace RecipeMicroservice.Application.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }

        public UserDto(string userName)
        {
            UserName = userName;
        }
    }
}