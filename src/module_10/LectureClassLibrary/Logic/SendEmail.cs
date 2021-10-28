/*namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using System;
    using System.Net.Mail;

    public class SendEmail : ISendable, IDisposable
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _senderEmail;
        public SendEmail(ISmtpClient smtpClient)
        {
            _smtpClient = smtpClient.GetClient();
            _senderEmail = smtpClient.GetSender();
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }

        public void Send(string address, string subject, string message)
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
    }
}
*/