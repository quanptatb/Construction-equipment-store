namespace VietMachWeb.Models.Entities
{
    public class ProductStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }            // draft, published, archived
        public string NormalizedName { get; set; }  // DRAFT, PUBLISHED

        public string Description { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
