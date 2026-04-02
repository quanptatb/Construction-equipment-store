namespace VietMachWeb.Models.Entities
{
    public class ProductDocument
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Title { get; set; }
        public int MediaId { get; set; }
        public string FileType { get; set; }

        public int SortOrder { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Media Media { get; set; }
    }
}
