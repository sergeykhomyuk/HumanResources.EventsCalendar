using System;
using System.Collections.Generic;
using System.Net.Mail;
using Microsoft.SharePoint;
using System.Linq;

namespace Framework.SharePoint.Services.Email
{
    public class EmailService : IEmailService
    {
        #region Implementation of IEmailService

        public void Send(SPWeb web, string emailTo, string senderDisplayName, string subject, string body)
        {
            Send(web, new[] {emailTo}, senderDisplayName, subject, body);
        }

        public void Send(SPWeb web, IEnumerable<string> emailTo, string senderDisplayName, string subject, string body)
        {
            if (web == null) throw new ArgumentNullException("web");
            if (emailTo == null || !emailTo.Any()) throw new ArgumentNullException("emailTo");

            var webApplication = web.Site.WebApplication;
            var from = new MailAddress(webApplication.OutboundMailSenderAddress, senderDisplayName);

            var message = new MailMessage
            {
                IsBodyHtml = true,
                Body = body,
                From = from
            };

            var smtpServer = webApplication.OutboundMailServiceInstance;
            var smtp = new SmtpClient(smtpServer.Server.Address);

            foreach (var email in emailTo)
            {
                message.To.Add(email);
            }

            message.Subject = subject;

            smtp.Send(message);
        }

        #endregion
    }
}