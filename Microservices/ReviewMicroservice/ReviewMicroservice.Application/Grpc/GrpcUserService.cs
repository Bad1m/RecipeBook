using Grpc.Core;
using ReviewMicroservice.Application.Grpc.Protos;
using ReviewMicroservice.Domain.Entities;
using ReviewMicroservice.Infrastructure.Interfaces;

namespace ReviewMicroservice.Application.Grpc
{
    public class GrpcUserService : GrpcUserReview.GrpcUserReviewBase
    {
        private readonly IUserRepository _userRepository;

        public GrpcUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<UserDto> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = new User
            {
                UserName = request.User.UserName
            };
            await _userRepository.InsertAsync(user);

            return request.User;
        }
    }
}