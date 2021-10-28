namespace BusinessLogic.Logic
{
    public record SenderConfiguration
    {
        public const string OptionsName = "SenderConfiguration";
        public string SmtpHost { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string TwilioSID { get; set; }
        public string TwilioAUTH { get; set; }
        public string SenderPhoneNumber { get; set; }
    }
}