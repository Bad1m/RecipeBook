using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Infrastructure.Data
{
    public class RecipeContext : DbContext
    {
        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Instruction> Instructions { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

        public RecipeContext(DbContextOptions<RecipeContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(recipeIngredient => new { recipeIngredient.RecipeId, recipeIngredient.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Recipe)
                .WithMany(recipe => recipe.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(recipeIngredient => recipeIngredient.Ingredient)
                .WithMany(ingredient => ingredient.RecipeIngredients)
                .HasForeignKey(recipeIngredient => recipeIngredient.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Instruction>()
                .HasOne(instruction => instruction.Recipe)
                .WithMany(recipe => recipe.Instructions)
                .HasForeignKey(instruction => instruction.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}