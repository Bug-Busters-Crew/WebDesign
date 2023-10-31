using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebDesign.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "ienikesergey@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp-relay.brevo.com", 587, false);
                await client.AuthenticateAsync("ienikesergey@gmail.com", "xsmtpsib-da0d195112b186ac811cadcffad4c1ab27a522cba566558587eff52586206415-L5Fs3gAHhtIm1CZp");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
