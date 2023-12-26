namespace AuthMicroservice.BusinessLogic.Constants
{
    public static class ErrorMessages
    {
        public const string UserNotFound = "User not found.";
        public const string UserNotFoundByLogin = "User with login '{0}' not found.";
        public const string AssignRoleFailed = "Failed to assign role '{0}' to user.";
        public const string UsernameAlreadyTaken = "Username is already taken.";
        public const string EmailAlreadyTaken = "Email is already taken.";
        public const string UserRegistrationFailed = "User registration failed.";
        public const string InvalidCredentials = "Invalid credentials.";
        public const string UserNotFoundForRefreshToken = "User not found for the given refresh token.";
        public const string UsernameRequired = "Username is required";
        public const string PasswordRequired = "Password is required";
        public const string FirstNameRequired = "First name is required";
        public const string LastNameRequired = "Last name is required";
        public const string EmailRequired = "Email is required";
        public const string InvalidEmail = "Invalid email address";
        public const string InvalidRoleSpecified = "Invalid role specified.";
    }
}