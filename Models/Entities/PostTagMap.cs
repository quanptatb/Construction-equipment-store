namespace VietMachWeb.Models.Entities
{
    public class PostTagMap
    {
        public int PostId { get; set; }
        public int TagId { get; set; }

        public Post Post { get; set; }
        public PostTag Tag { get; set; }
    }
}
