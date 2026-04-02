namespace VietMachWeb.Models.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<AuditLog> AuditLogs { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
