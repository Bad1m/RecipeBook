using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;
using System.Reflection;

namespace RecipeMicroservice.Infrastructure.Data
{
    public class RecipeContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}