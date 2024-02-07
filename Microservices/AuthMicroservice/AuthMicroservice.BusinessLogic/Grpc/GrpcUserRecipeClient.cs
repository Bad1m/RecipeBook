using AuthMicroservice.BusinessLogic.Grpc.Protos;
using AuthMicroservice.DataAccess.Entities;

namespace AuthMicroservice.BusinessLogic.Grpc
{
    public class GrpcUserRecipeClient
    {
        private readonly GrpcUserRecipe.GrpcUserRecipeClient _client;

        public GrpcUserRecipeClient(GrpcUserRecipe.GrpcUserRecipeClient client)
        {
            _client = client;
        }

        public async Task CreateUserAsync(User user)
        {
            var userDto = new UserDto { UserName = user.UserName };
            var request = new CreateUserRequest()
            {
                User = userDto
            };
            await _client.CreateUserAsync(request);
        }
    }
}