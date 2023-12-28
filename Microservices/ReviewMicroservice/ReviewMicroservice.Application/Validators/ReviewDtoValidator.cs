using FluentValidation;
using ReviewMicroservice.Domain.Constants;
using ReviewMicroservice.Domain.Dtos;

namespace ReviewMicroservice.Application.Validators
{
    public class ReviewDtoValidator : AbstractValidator<ReviewRequest>
    {
        public ReviewDtoValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty().WithMessage(ErrorMessages.RecipeIdIsRequired);
            RuleFor(x => x.Comment).NotEmpty().WithMessage(ErrorMessages.CommentIsRequired);
            RuleFor(x => x.Date).NotEmpty().WithMessage(ErrorMessages.DateIsRequired);
        }
    }
}