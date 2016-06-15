using System.Net;
using System.Net.Mail;
using System.Text;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class SendEmailViewModel
    {
        public MailViewModel MailViewModel { get; set; }
        public MailContentViewModel MailContentViewModel { get; set; }
        
        public static void SendMail(string smtpserver, int enablessl, string userName, string pwd, string nickName,
            string strfrom, string strto, string subj, string bodys)
        {
            var smtpClient = new SmtpClient
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = smtpserver,
                Credentials = new NetworkCredential(userName, pwd)
            };
            if (enablessl == 1)
            {
                smtpClient.EnableSsl = true;
            }

            var from = new MailAddress(strfrom, nickName);
            var to = new MailAddress(strto);
            var mailMessage = new MailMessage(from, to);
            mailMessage.Subject = subj;
            mailMessage.Body = bodys;
            mailMessage.BodyEncoding = Encoding.Default;
            mailMessage.IsBodyHtml = true;
            mailMessage.Priority = MailPriority.Normal;
            smtpClient.Send(mailMessage);
        }
    }
}