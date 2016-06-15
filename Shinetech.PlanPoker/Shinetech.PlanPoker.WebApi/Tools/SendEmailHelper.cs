using Shinetech.PlanPoker.Logic.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public class SendEmailHelper
    {
        public static bool SendEmail(SendEmailViewModel sendEmailViewModel)
        {

            var titletxt = sendEmailViewModel.MailContentViewModel.MailTitle;
            var bodytxt = sendEmailViewModel.MailContentViewModel.Content;

            bodytxt = bodytxt.Replace("{webname}", sendEmailViewModel.MailViewModel.WebName);
            bodytxt = bodytxt.Replace("{weburl}", sendEmailViewModel.MailViewModel.WebUrl);
            bodytxt = bodytxt.Replace("{webtel}", sendEmailViewModel.MailViewModel.WebTel);
            bodytxt = bodytxt.Replace("{linkurl}", sendEmailViewModel.MailViewModel.AbsUrl + "?code=" + TokenGenerator.EncodeToken(sendEmailViewModel.MailViewModel.EmailTo));

            try
            {
                SendEmailViewModel.SendMail(sendEmailViewModel.MailViewModel.EmailSmtp,
                    sendEmailViewModel.MailViewModel.EmailSsl,
                    sendEmailViewModel.MailViewModel.EmailUserName,
                    DesEncrypt.Decrypt(sendEmailViewModel.MailViewModel.EmailPassWord),
                    sendEmailViewModel.MailViewModel.EmailNickName,
                    sendEmailViewModel.MailViewModel.EmailFrom,
                    sendEmailViewModel.MailViewModel.EmailTo,
                    titletxt, bodytxt);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}