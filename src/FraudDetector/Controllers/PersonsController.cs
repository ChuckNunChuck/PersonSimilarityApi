using FraudDetector.Application.Common.Models;
using FraudDetector.Application.Persons.Commands;
using FraudDetector.Application.Persons.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

public class PersonsController : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<PersonDto>>> GetPersons(
        [FromQuery] GetPersonsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreatePersonCommand command)
    {
        return CommandResultToActionResult(await Mediator.Send(command));
    }
}