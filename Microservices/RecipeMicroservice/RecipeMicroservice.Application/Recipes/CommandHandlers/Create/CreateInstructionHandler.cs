using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Interfaces;
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

        private readonly IRecipeExistenceChecker _recipeExistenceChecker;

        private readonly ICacheRepository _cacheRepository;

        public CreateInstructionHandler(IInstructionRepository instructionRepository, 
            IMapper mapper, 
            IRecipeRepository recipeRepository, 
            IRecipeExistenceChecker recipeExistenceChecker, 
            ICacheRepository cacheRepository)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _recipeRepository = recipeRepository;
            _recipeExistenceChecker = recipeExistenceChecker;
            _cacheRepository = cacheRepository;
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
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            await _instructionRepository.SaveChangesAsync(cancellationToken);

            return _mapper.Map<InstructionDto>(createdInstruction);
        }
    }
}