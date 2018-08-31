using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace OMX.Web.Areas.Identity.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        private readonly string apiKey;

        public SendGridEmailSender(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var apiKey = this.apiKey;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("admin@omx.com", "OMX Admin");         
            var to = new EmailAddress(email, email);            
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, htmlMessage);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
