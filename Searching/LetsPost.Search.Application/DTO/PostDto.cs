namespace LetsPost.Search.Application.DTO
{
    public class PostDto
    {
        public Guid Id { get; set; }
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
