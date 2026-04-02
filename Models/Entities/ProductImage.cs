namespace VietMachWeb.Models.Entities
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int MediaId { get; set; }
        public Media Media { get; set; }
        public string AltText { get; set; }

        public int SortOrder { get; set; }
        public bool IsPrimary { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
