using FraudDetector.Application.Common.Enums;

namespace FraudDetector.Application.Common.Interfaces;

public interface ICommandResult
{
    CommandActionResult Result { get; set; }
}