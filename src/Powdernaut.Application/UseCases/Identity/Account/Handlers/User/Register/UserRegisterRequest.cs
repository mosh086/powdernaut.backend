namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;

public class UserRegisterRequest : RequestModel<UserRegisterResponse>
{
    public string Email { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string ConfirmPassword { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string[] Roles { get; set; } = [];
}
