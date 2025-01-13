using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Infrastructure.Configurations
{
    public class InstructionConfiguration : IEntityTypeConfiguration<Instruction>
    {
        public void Configure(EntityTypeBuilder<Instruction> builder)
        {
            builder.HasOne(instruction => instruction.Recipe)
                .WithMany(recipe => recipe.Instructions)
                .HasForeignKey(instruction => instruction.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}