namespace BusinessLogic.Logic
{
    using BusinessLogic.Exceptions;
    using BusinessLogic.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using System.Net;
    using System.Net.Mail;

    sealed internal class AppSmtpClient : ISmtpClient, IDisposable
    {
        private SmtpClient _smtpClient;
        private readonly string _senderEmail;
        private readonly SenderConfiguration _senderConfiguration;
        private bool disposed = false;
        public AppSmtpClient(IConfiguration configuration)
        {
            _senderConfiguration = configuration.GetSection(SenderConfiguration.OptionsName).Get<SenderConfiguration>();
            _senderEmail = _senderConfiguration.Email;
            _smtpClient = new SmtpClient(_senderConfiguration.SmtpHost)
            {
                Port = _senderConfiguration.Port,
                Credentials = new NetworkCredential(_senderEmail, _senderConfiguration.Password),
                EnableSsl = true,
            };
        }

        public void Dispose()
        {
            if (!disposed)
            {
                _smtpClient.Dispose();
                disposed = true;
            }
        }

        public void Send(string address, string subject, string message)
        {
            if (disposed) throw new SmtpClientDisposedException("Smtp client was disposed, unable to send message.");
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_senderEmail),
                Subject = subject,
                Body = message,
            };
            mailMessage.To.Add(address);
            _smtpClient.Send(mailMessage);
        }
    }
}
