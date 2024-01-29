using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateIngredientHandler : IRequestHandler<UpdateIngredientCommand, IngredientDto>
    {
        private readonly IIngredientRepository _ingredientRepository;

        private readonly IMapper _mapper;

        public UpdateIngredientHandler(IIngredientRepository ingredientRepository, IMapper mapper)
        {
            _ingredientRepository = ingredientRepository;
            _mapper = mapper;
        }

        public async Task<IngredientDto> Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var existingIngredient = await _ingredientRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingIngredient == null)
            {
                throw new ArgumentNullException(ErrorMessages.IngredientNotFound);
            }

            _mapper.Map(request, existingIngredient);

            await _ingredientRepository.UpdateAsync(existingIngredient, cancellationToken);
            var updatedIngredientDto = _mapper.Map<IngredientDto>(existingIngredient);

            return updatedIngredientDto;
        }
    }
}