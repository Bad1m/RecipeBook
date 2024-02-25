using Grpc.Core;
using RecipeMicroservice.Application.Grpc.Protos;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Grpc
{
    public class GrpcUserRecipeService : GrpcUserRecipe.GrpcUserRecipeBase
    {
        private readonly IUserRepository _userRepository;

        public GrpcUserRecipeService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<UserDto> CreateUser(CreateUserRequest request, ServerCallContext context)
        {
            var user = new User(request.User.UserName);
            await _userRepository.InsertAsync(user);
            await _userRepository.SaveChangesAsync();

            return request.User;
        }
    }
}