namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Net;
    using System.Net.Mail;

    public class SendEmail : ISendable, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;

        public SendEmail(IConfiguration configuration)
        {
            var senderConfiguration = configuration.GetSection(SenderConfiguration.OptionsName).Get<SenderConfiguration>();
            _senderEmail = senderConfiguration.Email;
            _smtpClient = new SmtpClient(senderConfiguration.SmtpHost)
            {
                Port = senderConfiguration.Port,
                Credentials = new NetworkCredential(_senderEmail, senderConfiguration.Password),
                EnableSsl = true,
            };
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }

        public void Send(string address, string subject, string message)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_senderEmail),
                    Subject = subject,
                    Body = message,
                };
                mailMessage.To.Add(address);
                _smtpClient.Send(mailMessage);
            }
            catch (SmtpException)
            {
                // TODO: HANDLE SMTP EXCEPTION
            }
        }
    }
}
