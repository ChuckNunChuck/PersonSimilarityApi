using FraudDetector.Domain.Common;

namespace FraudDetector.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}