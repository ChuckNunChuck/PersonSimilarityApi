using FraudDetector.Application.Enums;
using FraudDetector.Application.Model.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    public ISender Sender { get; }

    protected ApiControllerBase(ISender sender) => Sender = sender;

    protected IActionResult CommandResultToActionResult<T>(CommandResult<T> result)
        => result.Result switch
        {
            CommandActionResult.Success => Ok(result.Value),
            CommandActionResult.Created => Created("TODO", result.Value),
            CommandActionResult.Updated => NoContent(),
            CommandActionResult.Deleted => NoContent(),
            CommandActionResult.NotFound => NotFound(),
            _ => BadRequest(
                new ProblemDetails
                {
                    Title = "Unknown commandActionResult"
                })
        };
}