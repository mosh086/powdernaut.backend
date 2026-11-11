using Powdernaut.WebApi.Exceptions;

namespace Powdernaut.WebApi.Controllers.Common;

public class ExceptionController : BaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        //try
        //{
        //    throw new WebApiException("Throw Exception");
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        return Ok();
    }


    [HttpGet("Throw")]
    public IActionResult Throw()
    {
        //try
        //{
        //    throw new WebApiException("Throw Exception");
        //}
        //catch (Exception ex)
        //{
        //    throw ex;
        //}

        return Ok();
    }
}
