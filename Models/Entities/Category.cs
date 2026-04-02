namespace VietMachWeb.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        public ICollection<Category> Children { get; set; }
    }
}
