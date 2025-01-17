﻿using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateInstructionHandler : IRequestHandler<UpdateInstructionCommand, InstructionDto>
    {
        private readonly IInstructionRepository _instructionRepository;

        private readonly IMapper _mapper;

        private readonly ICacheRepository _cacheRepository;

        public UpdateInstructionHandler(IInstructionRepository instructionRepository, IMapper mapper, ICacheRepository cacheRepository)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
            _cacheRepository = cacheRepository;
        }

        public async Task<InstructionDto> Handle(UpdateInstructionCommand request, CancellationToken cancellationToken)
        {
            var existingInstruction = await _instructionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingInstruction == null)
            {
                throw new ArgumentNullException(ErrorMessages.InstructionNotFound);
            }

            _mapper.Map(request, existingInstruction);
            var duplicateStepNumber = await _instructionRepository.IsCheckDuplicateStepNumberAsync(
                (int)existingInstruction.RecipeId,
                (int)existingInstruction.StepNumber,
                existingInstruction.Id,
                cancellationToken);

            if (duplicateStepNumber)
            {
                throw new InvalidOperationException(ErrorMessages.StepNumberMustBeUnique);
            }

            await _instructionRepository.UpdateAsync(existingInstruction, cancellationToken);
            await _cacheRepository.RemoveAsync(CacheKeys.Recipes);
            var updatedInstructionDto = _mapper.Map<InstructionDto>(existingInstruction);

            return updatedInstructionDto;
        }
    }
}
