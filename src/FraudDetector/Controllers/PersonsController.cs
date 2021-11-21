using AutoMapper;
using FraudDetector.Application.Model;
using FraudDetector.Application.Persons.Commands;
using FraudDetector.Application.Persons.Queries;
using FraudDetector.ModelBinding.BodyAndRoute;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

public class PersonsController : ApiControllerBase
{
    public PersonsController(ISender mediator, IMapper mapper)
        : base(mediator, mapper)
    {
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedList<PersonDto>>> GetPersons(
        [FromQuery] GetPersonsQuery query, 
        CancellationToken cancellation) =>
        await Mediator.Send(query, cancellation);

    [HttpPost("{id}/calculate-similarity")]
    public async Task<IActionResult> CalculateSimilarity(
        [FromBodyAndRoute] CalculateSimilarityCommand command, 
        CancellationToken cancellation) =>
        CommandResultToActionResult(await Mediator.Send(command, cancellation));

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePersonCommand command, 
        CancellationToken cancellation) => 
        CommandResultToActionResult(await Mediator.Send(command, cancellation));


}