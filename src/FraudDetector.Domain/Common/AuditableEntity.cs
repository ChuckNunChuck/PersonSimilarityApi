namespace FraudDetector.Domain.Common;

public abstract class AuditableEntity<T>
{
    protected AuditableEntity(T id)
    {
        Id = id;
    }


    public T Id { get; protected set; }
    public DateTimeOffset Created { get; } = DateTimeOffset.UtcNow;
    public DateTimeOffset LastModified { get; protected set; } = DateTimeOffset.UtcNow;
}