namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.Role.RoleCreate;

public class RoleCreateValidator : AbstractValidator<RoleCreateRequest>
{
    public RoleCreateValidator()
    {
        //RuleFor(item => item.Email).EmailAddress().WithMessage("Email Is Not Correct Format ...");
        // Add other validation rules as needed
    }
}