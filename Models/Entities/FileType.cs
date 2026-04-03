namespace VietMachWeb.Models.Entities
{
    public class FileType
    {
        public int Id { get; set; }
        public string Name { get; set; } // PDF, DOCX

        public ICollection<ProductDocument> Documents { get; set; }
    }
}
