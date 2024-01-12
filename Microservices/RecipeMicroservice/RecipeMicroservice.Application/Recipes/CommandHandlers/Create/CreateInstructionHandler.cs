using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Domain.Entities;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Create
{
    public class CreateInstructionHandler : IRequestHandler<CreateInstructionForRecipeCommand, InstructionDto>
    {
        private readonly IInstructionRepository _instructionRepository;

        private readonly IMapper _mapper;

        private readonly IRecipeRepository _recipeRepository;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public CreateInstructionHandler(IInstructionRepository instructionRepository, IMapper mapper, IRecipeRepository recipeRepository, RecipeExistenceChecker recipeExistenceChecker)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<InstructionDto> Handle(CreateInstructionForRecipeCommand request, CancellationToken cancellationToken)
        {
            var recipe = await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.RecipeId, cancellationToken);

            if (recipe.Instructions.Any(instruction => instruction.StepNumber == request.StepNumber))
            {
                throw new InvalidOperationException(ErrorMessages.StepNumberMustBeUnique);
            }

            var newInstruction = _mapper.Map<Instruction>(request);
            var createdInstruction = await _instructionRepository.InsertAsync(newInstruction, cancellationToken);
            recipe.Instructions.Add(createdInstruction);
            await _recipeRepository.UpdateAsync(recipe, cancellationToken);
            await _instructionRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<InstructionDto>(createdInstruction);
        }
    }
}