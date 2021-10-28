namespace BusinessLogic.Logic
{
    using BusinessLogic.Interfaces;
    using Microsoft.Extensions.Configuration;
    using System;
    using Twilio;
    using Twilio.Rest.Api.V2010.Account;

    internal class AppTwilioClient : ITwilioClient
    {
        private readonly string _twilioSID;
        private readonly string _twilioAUTH;
        private readonly string _senderPhoneNumber;
        public AppTwilioClient(IConfiguration configuration)
        {
            var senderConfiguration = configuration.GetSection(SenderConfiguration.OptionsName).Get<SenderConfiguration>();
            _twilioSID = senderConfiguration.TwilioSID;
            _twilioAUTH = senderConfiguration.TwilioAUTH;
            _senderPhoneNumber = senderConfiguration.SenderPhoneNumber;
            TwilioClient.Init(_twilioSID, _twilioAUTH);
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
