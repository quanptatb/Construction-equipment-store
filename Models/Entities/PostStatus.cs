namespace VietMachWeb.Models.Entities
{
    public class PostStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}
