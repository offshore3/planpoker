using System.Net.Mail;

namespace Shinetech.PlanPoker.WebApi.ViewModels
{
    public class ShinetechMail
    {
        /// <summary>
        /// send email
        /// </summary>
        /// <param name="smtpserver">smtpserver</param>
        /// <param name="enablessl">enable ssl</param>
        /// <param name="userName">login acciunt</param>
        /// <param name="pwd">password</param>
        /// <param name="nickName">addressor nick name</param>
        /// <param name="strfrom">addressor</param>
        /// <param name="strto">receiver</param>
        /// <param name="subj">subject</param>
        /// <param name="bodys">content</param>
        public static void sendMail(string smtpserver, int enablessl, string userName, string pwd, string nickName, string strfrom, string strto, string subj, string bodys)
        {
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            _smtpClient.Host = smtpserver;
            _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);
            if (enablessl == 1)
            {
                _smtpClient.EnableSsl = true;
            }

            MailAddress _from = new MailAddress(strfrom, nickName);
            MailAddress _to = new MailAddress(strto);
            MailMessage _mailMessage = new MailMessage(_from, _to);
            _mailMessage.Subject = subj;
            _mailMessage.Body = bodys;
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;
            _mailMessage.IsBodyHtml = true;
            _mailMessage.Priority = MailPriority.Normal;
            _smtpClient.Send(_mailMessage);
        }
    }
}