﻿using MimeKit;
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

            emailMessage.From.Add(new MailboxAddress("Администрация сайта", "nikitarabcev69@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp-relay.brevo.com", 587, false);
                await client.AuthenticateAsync("nikitarabcev69@gmail.com", "hrbXDGRkfdc91Lzx");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}