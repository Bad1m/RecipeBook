using FluentValidation;
using ReviewMicroservice.Domain.Constants;
using ReviewMicroservice.Application.Dtos;

namespace ReviewMicroservice.Application.Validators
{
    public class ReviewRequestValidator : AbstractValidator<ReviewRequest>
    {
        public ReviewRequestValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty().WithMessage(ErrorMessages.RecipeIdIsRequired);

            RuleFor(x => x.Comment).NotEmpty().WithMessage(ErrorMessages.CommentIsRequired);

            RuleFor(x => x.Date).NotEmpty().WithMessage(ErrorMessages.DateIsRequired);

            RuleFor(x => x.Rating).InclusiveBetween(0, 10).WithMessage(ErrorMessages.RatingOutOfRange);
        }
    }
}