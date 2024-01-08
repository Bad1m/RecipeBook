using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Queries;
using RecipeMicroservice.Domain.Settings;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.QueryHandlers
{
    public class GetAllInstructionsByRecipeIdHandler : IRequestHandler<GetAllInstructionsByRecipeId, IEnumerable<InstructionDto>>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;

        public GetAllInstructionsByRecipeIdHandler(IInstructionRepository instructionRepository, IMapper mapper)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<InstructionDto>> Handle(GetAllInstructionsByRecipeId request, CancellationToken cancellationToken)
        {
            var pagination = new PaginationSettings
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
            var instructions = await _instructionRepository.GetInstructionsByRecipeIdAsync(request.RecipeId, pagination, cancellationToken);
            var instructionDtos = _mapper.Map<IEnumerable<InstructionDto>>(instructions);

            return instructionDtos;
        }
    }
}