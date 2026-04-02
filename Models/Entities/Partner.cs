namespace VietMachWeb.Models.Entities
{
    public class Partner
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        public string WebsiteUrl { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
