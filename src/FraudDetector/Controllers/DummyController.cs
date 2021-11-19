using Microsoft.AspNetCore.Mvc;

namespace FraudDetector.Controllers;

public class DummyController : ApiControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok();
}
