namespace Domain.Common;

public abstract class BaseEntity
{
    public virtual string Id { get; set; } = null!;
    public virtual DateTimeOffset Created { get; set; }
}
