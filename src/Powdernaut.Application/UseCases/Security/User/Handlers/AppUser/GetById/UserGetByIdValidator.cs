namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetById;

public class UserGetByIdValidator : AbstractValidator<UserGetByIdRequest>
{
    public UserGetByIdValidator()
    {
        // RuleFor(item => item.Email).EmailAddress().WithMessage("Email Is Not Correct Format ...");
        // Add other validation rules as needed
    }
}