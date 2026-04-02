namespace VietMachWeb.Models.Entities
{
    public class Contact
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

        public string Status { get; set; } // pending | resolved

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // 🔥 Reply flow
        public DateTime? RepliedAt { get; set; }
        public string ReplyContent { get; set; }

        public int? RepliedBy { get; set; }
        public User Replier { get; set; }
    }
}
