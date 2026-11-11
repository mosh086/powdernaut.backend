namespace Powdernaut.Application.UseCases.Identity.Account.Handlers.User.Register;

public class UserRegisterResponse : BaseDTO 
{
    public string Message { get; set; }

    public UserRegisterResponse(string message)
    {
        Message = message;
    }
}
