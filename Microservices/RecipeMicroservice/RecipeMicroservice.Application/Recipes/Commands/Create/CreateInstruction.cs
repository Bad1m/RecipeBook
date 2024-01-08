using MediatR;
using RecipeMicroservice.Application.Dtos;

namespace RecipeMicroservice.Application.Recipes.Commands.Create
{
    public class CreateInstruction : IRequest<InstructionDto>
    {
        public int StepNumber { get; set; }
        public string Description { get; set; }
    }
}