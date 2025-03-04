using Domain.Common;

namespace Domain.Entities
{
    public class Photo : BaseEntity
    {
        public string UserId { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; }
        public string? PublicId { get; set; }

        public AppUser User { get; set; } = null!;
    }
}