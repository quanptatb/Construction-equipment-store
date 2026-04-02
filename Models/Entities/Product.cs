namespace VietMachWeb.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public string Model { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public bool IsFeatured { get; set; }
        public int StatusId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public DateTime? ArchivedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public ProductStatus Status { get; set; }
        public ICollection<ProductSpec> Specs { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductDocument> Documents { get; set; }
    }
}
