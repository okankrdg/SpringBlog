using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace SpringBlog.Helpers
{
    public static class EmailServiceHelpers
    {
        public static  async Task SendEmailAsync(string destination, string subject, string body)
        {
            MailMessage mail = new MailMessage();
           
           
            mail.To.Add(destination);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            using (var smtpClient = new SmtpClient("okankaradag.com"))
            {
               await smtpClient.SendMailAsync(mail);
            }
           
        }
    }
}