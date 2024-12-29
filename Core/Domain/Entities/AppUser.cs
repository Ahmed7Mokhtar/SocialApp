using Domain.Common;

namespace Domain.Entities;

public class AppUser : BaseEntity
{
    public string UserName { get; set; } = null!;
}
