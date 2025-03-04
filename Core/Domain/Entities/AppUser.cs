using Domain.Common;
using Domain.Enums;
using Domain.Extenstions;

namespace Domain.Entities;

public class AppUser : BaseEntity
{
    public string UserName { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string? KnownAs { get; set; }
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public Gender Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public UserAddress? Address { get; set; }
    public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

    //public int GetAge() => DateOfBirth.CalcAge();
}
