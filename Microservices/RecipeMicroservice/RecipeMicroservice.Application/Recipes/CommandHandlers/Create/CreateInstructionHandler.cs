using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateInstructionHandler : IRequestHandler<CreateInstructionForRecipe, InstructionDto>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;
        private readonly IRecipeRepository _recipeRepository;

        public CreateInstructionHandler(IInstructionRepository instructionRepository, IMapper mapper, IRecipeRepository recipeRepository)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
        }

        public async Task<InstructionDto> Handle(CreateInstructionForRecipe request, CancellationToken cancellationToken)
        {
            var newInstruction = _mapper.Map<Instruction>(request);
            var createdInstruction = await _instructionRepository.InsertAsync(newInstruction, cancellationToken);

            if (request.RecipeId != null)
            {
                var recipe = await _recipeRepository.GetByIdAsync(request.RecipeId, cancellationToken);

                if (recipe != null)
                {
                    recipe.Instructions.Add(createdInstruction);
                    await _recipeRepository.UpdateAsync(recipe, cancellationToken);
                    await _instructionRepository.SaveChangesAsync(cancellationToken);
                }

                else
                {
                    throw new InvalidOperationException(ErrorMessages.RecipeNotFound);
                }
            }

            return _mapper.Map<InstructionDto>(createdInstruction);
        }
    }
}