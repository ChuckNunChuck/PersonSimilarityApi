using AutoMapper;
using FraudDetector.Application.Common.Models;
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

    [HttpGet("{id}/calculate-similarity")]
    public async Task<ActionResult<decimal>> GetSimilarity(
        [FromBodyAndRoute] GetSimilarityQuery query, 
        CancellationToken cancellation) =>
        await Mediator.Send(query, cancellation);

    [HttpPost]
    public async Task<IActionResult> Create(
        CreatePersonCommand command, 
        CancellationToken cancellation) => 
        CommandResultToActionResult(await Mediator.Send(command, cancellation));


}