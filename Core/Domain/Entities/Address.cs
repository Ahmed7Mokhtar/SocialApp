using Domain.Common;

namespace Domain.Entities
{
    public class UserAddress : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string? City { get; set; }
        public string? Country { get; set; }

        public AppUser User { get; set; } = null!;
    }
}