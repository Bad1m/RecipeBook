using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        Task<IEnumerable<Recipe>> GetRecipesByUserNameAsync(string userName, CancellationToken cancellationToken);
    }
}