using System.Net;
using System.Net.Mail;

namespace TestingModuleWebApp.Models
{
    public class MailerMessage
    {
        public MailAddress FromAdress { get; private set; }
        public MailAddress ToAdress { get; private set; }
        public string? Body { get; private set; }
        public string? Subject { get; private set; }
        public NetworkCredential Credential { get; private set; }

        public MailerMessage(MailAddress fromAdress, MailAddress toAdress, string body, string subject)
        {
            FromAdress = fromAdress;
            ToAdress = toAdress;
            Body = body;
            Subject = subject;
        }
    }
}
