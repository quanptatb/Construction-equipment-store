namespace VietMachWeb.Configurations
{
    public class EmailSettings
    {
        public string SmtpHost { get; set; }      // smtp.gmail.com
        public int SmtpPort { get; set; }         // 587

        public string Address { get; set; }       // email gửi
        public string Password { get; set; }      // app password

        public string DisplayName { get; set; }   // tên hiển thị (VietMach)

        public bool UseSSL { get; set; }
    }
}
