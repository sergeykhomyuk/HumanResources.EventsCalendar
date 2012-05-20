using System.Collections.Generic;
using Microsoft.SharePoint;

namespace Framework.SharePoint.Services.Email
{
    public interface IEmailService
    {
        void Send(SPWeb web, string emailTo, string senderDisplayName, string subject, string body);
        void Send(SPWeb web, IEnumerable<string> emailTo, string senderDisplayName, string subject, string body);
    }
}