using Powdernaut.Domain.Exceptions;

namespace Powdernaut.Infrastructure.Exceptions;

public class InfraException : BaseException
{
    public InfraException(string message, params string[] parameters) : base(message, parameters)
    {
    }
}
public class IdentityException : InfraException
{
    public IEnumerable<string> Errors { get; set; }

    public IdentityException(IEnumerable<IdentityError> errors)
        : base("Identity operation failed")
    {
        Errors = errors.Select(e => e.Description);
    }

    public IdentityException(string error)
        : base("Identity operation failed")
    {
        Errors = new List<string> { error };
    }
}