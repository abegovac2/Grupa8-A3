using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace OOAD_Projekat.Utils
{
    public class MailSender : IMailSender
    {
        private readonly SmtpClient smtpClient;
        private readonly IOptions<MailInfo> mailInfo;
        public MailSender(IOptions<MailInfo> mailInfo)
        {
            this.smtpClient = new SmtpClient();
            this.mailInfo = mailInfo;
        }
        public void Send(string name, string email, string message)
        {
            var msg = new MimeMessage();
            msg.From.Add(new MailboxAddress("Dont Reply", mailInfo.Value.mail));
            msg.To.Add(new MailboxAddress(name, email));
            msg.Subject = "Login information - Dont reply";
            msg.Body = new TextPart("plain")
            {
                Text = message
            };
            smtpClient.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtpClient.Authenticate(mailInfo.Value.mail, mailInfo.Value.password);
            smtpClient.Send(msg);
            smtpClient.Disconnect(true);

        }
    }
}
