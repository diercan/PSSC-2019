using System;
using SendGrid;

namespace EmailSender.Config
{
    public class SendgridConfig
    {
        public readonly SendGridClient Client; 

        public SendgridConfig()
        {
            var apiKey = GetOrThrow();
            Client = new SendGridClient(apiKey);
        }
        
        private string GetOrThrow()
        {
            return "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";
        }
    }
}