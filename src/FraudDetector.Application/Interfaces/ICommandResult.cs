using FraudDetector.Application.Enums;

namespace FraudDetector.Application.Interfaces;

public interface ICommandResult
{
    CommandActionResult Result { get; set; }
}