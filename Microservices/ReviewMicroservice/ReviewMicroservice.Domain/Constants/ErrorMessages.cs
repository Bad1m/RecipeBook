namespace ReviewMicroservice.Domain.Constants
{
    public static class ErrorMessages
    {
        public const string RecipeIdIsRequired = "RecipeId is required.";
        public const string CommentIsRequired = "Comment is required.";
        public const string DateIsRequired = "Date is required.";
        public const string ReviewNotFound = "Review not found.";
        public const string RecipeNotFound = "Recipe not found.";
        public const string UserNotFound = "User not found.";
        public const string RatingOutOfRange = "Rating must be between 0 and 10.";
    }
}