namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetAll;

public class UserGetAllResponse
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}