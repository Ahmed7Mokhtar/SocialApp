using Domain.Common;

namespace Domain.Entities;

public class AppUser : BaseEntity
{
    public string UserName { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
}
