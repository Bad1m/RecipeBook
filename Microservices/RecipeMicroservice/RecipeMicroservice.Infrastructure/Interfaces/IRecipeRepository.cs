using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Domain.Models;
using RecipeMicroservice.Domain.Settings;

namespace RecipeMicroservice.Infrastructure.Interfaces
{
    public interface IRecipeRepository : IRepository<Recipe>
    {
        new Task<PaginatedResult<Recipe>> GetAllAsync(PaginationSettings pagination, CancellationToken cancellationToken);
        Task<PaginatedResult<Recipe>> GetRecipesByUserNameAsync(string userName, PaginationSettings paginationSettings, CancellationToken cancellationToken);
    }
}