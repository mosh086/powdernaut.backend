namespace Powdernaut.Application.UseCases.Security.Role.Handlers.AppRole.GetAll;

public class RoleGetAllValidator : AbstractValidator<RoleGetAllRequest>
{
    public RoleGetAllValidator()
    {
        //RuleFor(item => item.Email).EmailAddress().WithMessage("Email Is Not Correct Format ...");
        // Add other validation rules as needed
    }
}
