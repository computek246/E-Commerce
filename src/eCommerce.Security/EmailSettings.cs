namespace eCommerce.Security
{
    public class EmailSettings
    {
        public bool EnableSendEmail { get; set; }
        public string FromEmailName { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}
