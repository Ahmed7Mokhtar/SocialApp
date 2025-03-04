using System.ComponentModel.DataAnnotations;

namespace Domain.Common;

public abstract class BaseEntity
{
    [Key]
    [MaxLength(100)]
    public virtual string Id { get; set; } = null!;
    public virtual DateTimeOffset Created { get; set; }
}
