using AutoMapper;
using MediatR;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Constants;
using RecipeMicroservice.Infrastructure.Interfaces;

namespace RecipeMicroservice.Application.Recipes.CommandHandlers.Update
{
    public class UpdateInstructionHandler : IRequestHandler<UpdateInstruction, InstructionDto>
    {
        private readonly IInstructionRepository _instructionRepository;
        private readonly IMapper _mapper;

        public UpdateInstructionHandler(IInstructionRepository instructionRepository, IMapper mapper)
        {
            _instructionRepository = instructionRepository;
            _mapper = mapper;
        }

        public async Task<InstructionDto> Handle(UpdateInstruction request, CancellationToken cancellationToken)
        {
            var existingInstruction = await _instructionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (existingInstruction == null)
            {
                throw new ArgumentNullException(ErrorMessages.InstructionNotFound);
            }

            _mapper.Map(request, existingInstruction);
            await _instructionRepository.UpdateAsync(existingInstruction, cancellationToken);
            var updatedInstructionDto = _mapper.Map<InstructionDto>(existingInstruction);

            return updatedInstructionDto;
        }
    }
}
