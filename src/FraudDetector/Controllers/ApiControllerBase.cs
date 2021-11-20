using FraudDetector.Application.Common.Enums;
using FraudDetector.Application.Common.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    protected IActionResult CommandResultToActionResult<T>(CommandResult<T> result)
        => result.Result switch
        {
            CommandActionResult.Success => NoContent(),
            CommandActionResult.Created => Ok(result.Value),
            CommandActionResult.Updated => Ok(result.Value),
            CommandActionResult.Deleted => NoContent(),
            CommandActionResult.NotFound => NotFound(),
            _ => BadRequest(
                new ProblemDetails
                {
                    Title = "Unknown commandActionResult"
                })
        };
}