using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeKeepingApp.Services
{
    public class EmailSender
    {
        public void sendEmail(string toEmail, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("EmailName"));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = "Confirm your account";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            {
                smtp.Connect("smtp.gmail.com", 587);//, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("CS5525projectemailer@gmail.com", "nkwxntmovysgvbsl");
                smtp.Send(email);
                smtp.Disconnect(true);
            }
        }
    }
}