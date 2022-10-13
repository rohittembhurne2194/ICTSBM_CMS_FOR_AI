using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

namespace SwachhBharatAbhiyan.CMS.Models.Generics
{
    public class MailMessageGenric
    {
        public void SendMessage(Email email)
        {
            string Sender = WebConfigurationManager.AppSettings["EmailSender"].ToString();
            string Password = WebConfigurationManager.AppSettings["EmailPassword"].ToString();

            MailMessage mailMessage = new System.Net.Mail.MailMessage();
            // Specifing mail sender email address
            mailMessage.From = new MailAddress(Sender);
            // Adding subject to the emails
            mailMessage.Subject = email.Subject;
            // Adding body to the emails
            mailMessage.Body = email.Body;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(email.EmailTo);

            // Specifying the port and host enviorment
            SmtpClient SmtpClients = new SmtpClient();
            SmtpClients.DeliveryMethod = SmtpDeliveryMethod.Network;
            SmtpClients.Host = "smtp.appynitty.com";
            SmtpClients.Port = 25;
            SmtpClients.Credentials = new System.Net.NetworkCredential(Sender, Password);
            SmtpClients.EnableSsl = true;
            SmtpClients.Send(mailMessage);
        }
    }

    public class Email
    {

        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Sender { get; set; }
    }
}