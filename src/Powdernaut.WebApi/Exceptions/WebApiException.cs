using Powdernaut.Domain.Exceptions;

namespace Powdernaut.WebApi.Exceptions;

public class WebApiException : BaseException
{
    public WebApiException(string msg) : base(msg)
    {
    }

    public WebApiException(Exception exception) : base(exception)
    {
    }

    public WebApiException(string message, params string[] parameters) : base(message, parameters)
    {
    }

    public WebApiException(string msg, Exception exception) : base(msg, exception)
    {
    }
}