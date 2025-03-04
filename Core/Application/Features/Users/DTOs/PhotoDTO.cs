namespace Application.Features.Users.DTOs
{
    public class PhotoDTO
    {
        public string Id { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsMain { get; set; }
    }
}
