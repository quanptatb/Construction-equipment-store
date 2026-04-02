namespace VietMachWeb.Models.Entities
{
    public class Setting
    {
        public int Id { get; set; } 
        public string Key { get; set; }
        public string Value { get; set; }

        public string Group { get; set; }
        public string Description { get; set; }
    }
}
