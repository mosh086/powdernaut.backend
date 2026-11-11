using IResult = Powdernaut.Application.Common.Models.Requests.IResult;

namespace Powdernaut.WebApi.Common.Models;

//public class ApiResult
//{
//    public static ApiResult<T> Success<T>(T data)
//    {
//        return new ApiResult<T>()
//        {
//            Data = data,
//            Error = null!,
//            Message = new List<string> { "Success" },
//            Success = true,
//            //Token = ""
//        };
//    }

//    public static ApiResult<T> Return<T>(T data)
//    {
//        if (data is null || data is false)
//            return Faild(data);
//        return Success(data);
//    }

//    public static ApiResult<T> Faild<T>(T data)
//    {
//        return new ApiResult<T>()
//        {
//            Data = data,
//            Error = null!,
//            Message = new List<string> { "Faild" },
//            Success = false,
//            //Token = ""
//        };
//    }
    
//    public static ApiResult<bool> UnAuthorize()
//    {
//        return new ApiResult<bool>()
//        {
//            Data = false,
//            Error = null!,
//            Message = new List<string> { "Unauthorized access. Please log in." },
//            Success = false
//        };
//    }
//}
//public class ApiResult<T> : ApiResult, IResult
//{
//    public bool Success { get; set; }
//    public int Status { get; set; }
//    public T Data { get; set; } = default!;
//    public IEnumerable<string>? Message { get; set; }
//    public Exception Error { get; set; } = default!;
//}