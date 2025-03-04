using Domain.Enums;

namespace Application.Features.Users.DTOs;

public class MemberDTO
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public int Age { get; set; }
    public string? KnownAs { get; set; }
    public DateTimeOffset Created { get; set; }
    public DateTime LastActive { get; set; }
    public Gender Gender { get; set; }
    public string? Introduction { get; set; }
    public string? LookingFor { get; set; }
    public string? Interests { get; set; }
    public AddressDTO? Address { get; set; }
    public List<PhotoDTO> Photos { get; set; } = new List<PhotoDTO>();
}
