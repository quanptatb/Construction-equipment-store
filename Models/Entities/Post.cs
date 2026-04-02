using Microsoft.AspNetCore.Authentication.Cookies;

namespace VietMachWeb.Models.Entities
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }

        public int ThumbnailId { get; set; }
        public Media Thumbnail { get; set; }

        public int AuthorId { get; set; }
        public User Author { get; set; }

        public int StatusId { get; set; } // draft | published | archived
        public PostStatus Status { get; set; }

        public DateTime? PublishedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<PostTagMap> PostTags { get; set; }
    }
}
