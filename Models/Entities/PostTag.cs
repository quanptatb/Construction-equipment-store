namespace VietMachWeb.Models.Entities
{
    public class PostTag
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<PostTagMap> PostTags { get; set; }
    }
}
