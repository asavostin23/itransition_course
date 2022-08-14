using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace Course.Services
{
    public class MailRuEmailSender : IEmailSender
    {
        protected SmtpClient SmtpClient { get; set; }
        protected const string EmailFrom = "course.asavostin@mail.ru";
        public MailRuEmailSender(IConfiguration configuration)
        {
            SmtpClient = new SmtpClient("smtp.mail.ru", 587);
            SmtpClient.Credentials = new NetworkCredential(EmailFrom, configuration["Passwords:Email"]);
            SmtpClient.EnableSsl = true;
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage message = new MailMessage(new MailAddress(EmailFrom, "CourseServer"), new MailAddress(email));
            message.Subject = subject;
            message.Body = htmlMessage;
            message.IsBodyHtml = true;
            try
            {
                await SmtpClient.SendMailAsync(message);
            }
            catch (SmtpFailedRecipientsException ex)
            { }
        }
    }
}
