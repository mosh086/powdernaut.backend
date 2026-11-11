using Powdernaut.Domain.Exceptions;

namespace Powdernaut.Application.Exceptions;

public class AppException : BaseException
{
    public AppException(string message) : base(message)
    {

    }
    public AppException(string message, Exception exception) : base(message, exception)
    {

    }

    public AppException(BaseException exception) : base(exception.Message, exception)
    {

    }
}