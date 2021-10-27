namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    public class SendSMS : ISendable
    {
        private readonly string _twilioSID;
        private readonly string _twilioAUTH;
        private readonly string _senderPhoneNumber;
        public SendSMS(IConfiguration configuration)
        {
            var senderConfiguration = configuration.GetSection(SenderConfiguration.OptionsName).Get<SenderConfiguration>();
            _twilioSID = senderConfiguration.TwilioSID;
            _twilioAUTH = senderConfiguration.TwilioAUTH;
            _senderPhoneNumber = senderConfiguration.SenderPhoneNumber;
        }
        public void Send(string address, string subject, string message)
        {
            TwilioClient.Init(_twilioSID, _twilioAUTH);
            MessageResource.Create(
                body: $"{subject}{Environment.NewLine}{message}",
                from: new Twilio.Types.PhoneNumber(_senderPhoneNumber),
                to: new Twilio.Types.PhoneNumber(address)
             );

        }
    }
}
