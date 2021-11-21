using FraudDetector.Application.Model;
using FraudDetector.Application.Persons.Commands.CalculateSimilarityCommand;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Application.Persons.Queries;
using FraudDetector.Application.Persons.Queries.GetPersonsQuery;
using FraudDetector.ModelBinding.BodyAndRoute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

public class PersonsController : ApiControllerBase
{
    public PersonsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PersonDto>>> GetPersons(
        [FromQuery] GetPersonsQuery query, 
        CancellationToken cancellation) =>
        await Sender.Send(query, cancellation);

    [HttpPost("{id}/calculate-similarity")]
    public async Task<IActionResult> CalculateSimilarity(
        [FromBodyAndRoute] CalculateSimilarityCommand command, 
        CancellationToken cancellation) =>
        CommandResultToActionResult(await Sender.Send(command, cancellation));

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePersonCommand command, 
        CancellationToken cancellation) => 
        CommandResultToActionResult(await Sender.Send(command, cancellation));


}