using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Update
{
    public class UpdateInstructionCommand : IRequest<InstructionDto>
    {
        public int Id { get; set; }
        public int StepNumber { get; set; }
        public string Description { get; set; }
    }
}