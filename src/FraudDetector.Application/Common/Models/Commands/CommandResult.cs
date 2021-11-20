using FraudDetector.Application.Common.Enums;
using FraudDetector.Application.Common.Interfaces;

namespace FraudDetector.Application.Common.Models.Commands;

public class CommandResult<T> : ICommandResult
{
    public T? Value { get; init; }

    public static CommandResult<T> NotFound => new()
    {
        Result = CommandActionResult.NotFound
    };

    public static CommandResult<T> Success => new()
    {
        Result = CommandActionResult.Success
    };

    public CommandActionResult Result { get; set; }
}