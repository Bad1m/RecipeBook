using AuthMicroservice.BusinessLogic.Grpc.Protos;
using AuthMicroservice.DataAccess.Entities;

namespace AuthMicroservice.BusinessLogic.Grpc
{
    public class GrpcUserReviewClient
    {
        private readonly GrpcUserReview.GrpcUserReviewClient _client;

        public GrpcUserReviewClient(GrpcUserReview.GrpcUserReviewClient client)
        {
            _client = client;
        }

        public async Task CreateUser(User user)
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