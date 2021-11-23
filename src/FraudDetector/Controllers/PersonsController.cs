using FraudDetector.Application.Model;
using FraudDetector.Application.Persons.Commands.CalculateSimilarityCommand;
using FraudDetector.Application.Persons.Commands.CreatePersonCommand;
using FraudDetector.Application.Persons.Queries;
using FraudDetector.Application.Persons.Queries.GetPersonsQuery;
using FraudDetector.Authorization;
using FraudDetector.ModelBinding.BodyAndRoute;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

public class PersonsController : ApiControllerBase
{
    public PersonsController(ISender sender)
        : base(sender)
    {
    }

    [HttpGet]
    [Authorize(Policy = AuthorizationPolicyKeys.CanRead)]
    [ProducesResponseType(typeof(PaginatedList<PersonDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PaginatedList<PersonDto>>> GetPersons(
        [FromQuery] GetPersonsQuery query, 
        CancellationToken cancellation) =>
        await Sender.Send(query, cancellation);

    [HttpPost("{id}/calculate-similarity")]
    [Authorize(Policy = AuthorizationPolicyKeys.CanWrite)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CalculateSimilarity(
        [FromBodyAndRoute] CalculateSimilarityCommand command, 
        CancellationToken cancellation) =>
        CommandResultToActionResult(await Sender.Send(command, cancellation));

    [HttpPost]
    [Authorize(Policy = AuthorizationPolicyKeys.CanWrite)]
    [ProducesResponseType(typeof(decimal), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        CreatePersonCommand command, 
        CancellationToken cancellation) => 
        CommandResultToActionResult(await Sender.Send(command, cancellation));


}