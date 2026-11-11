namespace Powdernaut.Application.Common.Models.Requests;

public interface IRequestModel : IRequest { }
public abstract class RequestModel : IRequestModel { }


public interface IRequestModel<TResponse> : IRequest<TResponse> { } // where TResponse : IResult { }
public abstract class RequestModel<TResponse> : IRequestModel<TResponse> { } // where TResponse : IResult { }

public interface IResult
{
    //bool Success { get; }
    //int Status { get; }
    //IEnumerable<string>? Errors { get; }

    bool Success { get; set; }
    int Status { get; set; }
    IEnumerable<string>? Message { get; set; }
    Exception Error { get; set; }
}

public sealed class Result<T> //: IResult
{
    public bool Success { get; set; }
    public int Status { get; set; }
    public IEnumerable<string>? Message { get; set; }
    public Exception Error { get; set; }
    public T? Value { get; private set; }

    private Result() { }

    public static Result<T> Ok(T value) => new() { Success = true, Status = 200, Value = value };
    public static Result<T> Fail(int status, params string[] errors) =>
        new() { Success = false, Status = status, Message = errors };
}