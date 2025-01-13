using AutoMapper;
using RecipeMicroservice.Application.Dtos;
using RecipeMicroservice.Application.Recipes.Commands.Create;
using RecipeMicroservice.Application.Recipes.Commands.Update;
using RecipeMicroservice.Domain.Entities;

namespace RecipeMicroservice.Application.Mappings
{
    public class InstructionMappingProfile : Profile
    {
        public InstructionMappingProfile()
        {
            CreateMap<CreateInstructionCommand, Instruction>();

            CreateMap<CreateInstructionForRecipeCommand, Instruction>();

            CreateMap<UpdateInstructionCommand, Instruction>();

            CreateMap<Instruction, InstructionDto>();
        }
    }
}