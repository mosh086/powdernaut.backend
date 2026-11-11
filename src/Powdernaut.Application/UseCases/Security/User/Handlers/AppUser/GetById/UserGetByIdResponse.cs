namespace Powdernaut.Application.UseCases.Security.User.Handlers.AppUser.GetById;

public class UserGetByIdResponse : BaseDTO
{
    public string Name { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}