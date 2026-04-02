namespace VietMachWeb.Models.Entities
{
    public class Media
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public string Url { get; set; }

        public long Size { get; set; }
        public string MimeType { get; set; }

        public int UploadedBy { get; set; }
        public User Uploader { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Banner> Banners { get; set; } // 👈 thêm navigation ngược
        public ICollection<ProductDocument> ProductDocuments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Partner> Partners { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
