#nullable disable
namespace BusinessLogic.Logic
{
    public record SenderConfiguration
    {
        public const string OptionsName = "SenderConfiguration";
        public string SmtpHost { get; init; }
        public int Port { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string TwilioSID { get; init; }
        public string TwilioAUTH { get; init; }
        public string SenderPhoneNumber { get; init; }
    }
}