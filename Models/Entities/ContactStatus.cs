namespace VietMachWeb.Models.Entities
{
    public class ContactStatus
    {
        public int Id { get; set; }
        public string Name { get; set; } // pending, resolved, rejected

        public ICollection<Contact> Contacts { get; set; }
    }
}
