namespace Powdernaut.Infrastructure.Identity.Parameters;

public class UserCreateParameters
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string PhoneNumber { get; set; }
    public UserCreateParameters(
        string userName,
        string password,
        string name,
        string email,
        string phoneNumber
        )
    {
        UserName = userName;
        Password = password;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}