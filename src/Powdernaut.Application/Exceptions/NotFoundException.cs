using Powdernaut.Domain.Exceptions;

namespace Powdernaut.Application.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message)
    {

    }
    public NotFoundException(Exception exception) : base(exception.Message, exception)
    {

    }
}