namespace VietMachWeb.Models.Entities
{
    public class ProductSpec
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int SpecKeyId { get; set; }
        public SpecKey SpecKey { get; set; }

        public string SpecValue { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
