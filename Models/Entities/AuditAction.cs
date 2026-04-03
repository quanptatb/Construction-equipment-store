namespace VietMachWeb.Models.Entities
{
    public class AuditAction
    {
        public int Id { get; set; }
        public string Name { get; set; } // CREATE, UPDATE, DELETE, RESTORE
        public string NormalizedName { get; set; } // CREATE, UPDATE
    }
}
