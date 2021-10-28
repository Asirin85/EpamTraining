/*namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using System;
    using Twilio.Rest.Api.V2010.Account;

    public class SendSMS : ISendable
    {
        private readonly string _senderPhoneNumber;
        public SendSMS(ITwilioClient twilioClient)
        {
            _senderPhoneNumber = twilioClient.GetSender();
        }
        public void Send(string address, string subject, string message)
        {
            MessageResource.Create(
                body: $"{subject}{Environment.NewLine}{message}",
                from: new Twilio.Types.PhoneNumber(_senderPhoneNumber),
                to: new Twilio.Types.PhoneNumber(address)
             );
        }
    }
}
*/