using AutoMapper;
using FraudDetector.Application.Common.Enums;
using FraudDetector.Application.Common.Models.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    public ISender Mediator { get; }
    public IMapper Mapper { get; }

    protected ApiControllerBase(ISender mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

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