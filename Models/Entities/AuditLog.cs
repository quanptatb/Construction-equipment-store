namespace VietMachWeb.Models.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ActionId { get; set; }
        public int EntityTypeId { get; set; }
        public int EntityId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IpAddress { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



        // Relation
        public User User { get; set; }
        public AuditAction Action { get; set; }
        public AuditEntityType EntityType { get; set; }

    }
}
