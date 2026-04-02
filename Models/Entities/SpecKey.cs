namespace VietMachWeb.Models.Entities
{
    public class SpecKey
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<ProductSpec> ProductSpecs { get; set; }
    }
}
