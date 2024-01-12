using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Helpers;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;
using RecipeMicroservice.Infrastructure.Repositories;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllInstructionsByRecipeIdHandler : IRequestHandler<GetAllInstructionsByRecipeIdQuery, IEnumerable<InstructionDto>>
    {
        private readonly IInstructionRepository _instructionRepository;

        private readonly IMapper _mapper;

        private readonly RecipeExistenceChecker _recipeExistenceChecker;

        public GetAllInstructionsByRecipeIdHandler(IInstructionRepository instructionRepository, IMapper mapper, RecipeExistenceChecker recipeExistenceChecker)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _recipeExistenceChecker = recipeExistenceChecker;
        }

        public async Task<IEnumerable<InstructionDto>> Handle(GetAllInstructionsByRecipeIdQuery request, CancellationToken cancellationToken)
        {
            await _recipeExistenceChecker.CheckRecipeExistenceAsync(request.RecipeId, cancellationToken);
            var instructions = await _instructionRepository.GetInstructionsByRecipeIdAsync(request.RecipeId, request.PaginationSettings, cancellationToken);
            var instructionDtos = _mapper.Map<IEnumerable<InstructionDto>>(instructions);

            return instructionDtos;
        }
    }
}