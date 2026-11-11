using Elastic.CommonSchema;
using Powdernaut.WebApi.Extensions;
using IResult = Powdernaut.Application.Common.Models.Requests.IResult;

namespace Powdernaut.WebApi.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : ControllerBase
{
    protected ProviderServices ProviderServices => HttpContext.ApplicationContext();

    protected virtual async Task<IActionResult> RequestAsync<TCommand, TCommandResult>(TCommand command)
        where TCommand : RequestModel<TCommandResult> //where TCommandResult : IResult
    {
        var result = await ProviderServices.Mediator.Send(command);
        if (result == null) return NotFound();
        //switch (result.Status)
        //{
        //    case 200:
        return Ok(result);
        //    case 400:
        //        return BadRequest(result.Message);
        //    case 404:
        //        return NotFound(result.Message);
        //    default:
        //        return BadRequest();
        //}
    }

    protected virtual async Task<IActionResult> RequestAsync<TCommand>(TCommand command)
      where TCommand : RequestModel
    {
        await ProviderServices.Mediator.Send(command);
        return Ok(true);
    }

    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
       => base.Ok(value);

    public override NotFoundObjectResult NotFound([ActionResultObjectValue] object? value)
       => base.NotFound(value);

    public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? value)
       => base.BadRequest(value);
}