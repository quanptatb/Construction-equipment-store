namespace VietMachWeb.Models.Entities
{
    public class Banner
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }

        public int MediaId { get; set; }
        public string LinkUrl { get; set; }

        public string ButtonText { get; set; }

        public int SortOrder { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Media Media { get; set; } // 👈 thêm navigation

    }
}
