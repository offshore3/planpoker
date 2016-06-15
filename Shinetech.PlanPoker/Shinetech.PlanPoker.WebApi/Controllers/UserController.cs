using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;
using System.Net.Mail;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class UserController : BaseController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic) : base(userLogic)
        {
            _userLogic = userLogic;
        }

        [HttpPost]
        [Route("user")]
        public void Create(UserViewModel userViewModel)
        {
            var userLogicModel = userViewModel.ToLogicModel();
            _userLogic.Create(userLogicModel);
        }

        [HttpGet]
        [Route("users")]
        public List<UserViewModel> GetUsers()
        {
            return _userLogic.GetAll().Select(x => new UserViewModel
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.Password
            }).ToList();
        }

        [HttpGet]
        [Route("checkemail")]
        public bool CheckEmailExist(string email)
        {
            return _userLogic.CheckEmailExist(email);
        }

        [HttpGet]
        [Route("login")]
        public string Login(string email, string password)
        {
            return _userLogic.Login(email, password);
        }

        [HttpGet]
        [Route("user")]
        [BasicAuthorize]
        public UserViewModel GetUserViewModel()
        {
            return LoginUser;
        }

        [HttpPut]
        [Route("user")]
        [BasicAuthorize]
        public void EditUser(UserViewModel userViewModel)
        {
            _userLogic.Edit(userViewModel.ToLogicModel());
        }

        [HttpGet]
        [Route("get-user-by-email")]
        public UserViewModel GetUserByEmail(string email)
        {
           return _userLogic.GetUserByEmail(email).ToViewModel();
        }

        [HttpPut]
        [Route("reset-user-passowrd")]
        public void ResetPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());
        }

        [HttpPut]
        [Route("changepassword")]
        [BasicAuthorize]
        public string EditUserPassword(UserViewModel userViewModel)
        {
            _userLogic.EditPassword(userViewModel.ToLogicModel());

            return Login(userViewModel.Email, userViewModel.Password);
        }

        [HttpPost]
        [Route("sendemail")]
        public bool SendEmail(MailModel mailModel,MailContentModel mailContentModel)
        {
            //替换模板内容
            var titletxt = mailContentModel.MailTitle;
            var bodytxt = mailContentModel.Content;
            titletxt = titletxt.Replace("{webname}", mailModel.WebName);
            titletxt = titletxt.Replace("{username}", "hello");
            bodytxt = bodytxt.Replace("{webname}", mailModel.WebName);
            bodytxt = bodytxt.Replace("{weburl}", mailModel.WebUrl);
            bodytxt = bodytxt.Replace("{webtel}", mailModel.WebTel);
            bodytxt = bodytxt.Replace("{username}", "hello");
            //bodytxt = bodytxt.Replace("{linkurl}", "http://" + HttpContext.Current.Request.Url.Authority.ToLower()
            //    + new BasePage().getlink(site, new BasePage().linkurl("repassword", "?action=email&code=" + codeModel.str_code)));
            //发送邮件

            try
            {
                DTMail.sendMail(mailModel.EmailSmtp,
                    mailModel.EmailSsl,
                    mailModel.EmailUserName,
                    DESEncrypt.Decrypt(mailModel.EmailPassWord),
                    mailModel.EmailNickName,
                    mailModel.EmailFrom,
                    mailModel.EmailTo,
                    titletxt, bodytxt);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
    }

    public class DTMail
    {
        #region 发送电子邮件
        /// <summary>
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpserver">SMTP服务器</param>
        /// <param name="enablessl">是否启用SSL加密</param>
        /// <param name="userName">登录帐号</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="strfrom">发件人</param>
        /// <param name="strto">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        public static void sendMail(string smtpserver, int enablessl, string userName, string pwd, string nickName, string strfrom, string strto, string subj, string bodys)
        {
            SmtpClient _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = smtpserver;//指定SMTP服务器
            _smtpClient.Credentials = new System.Net.NetworkCredential(userName, pwd);//用户名和密码
            if (enablessl == 1)
            {
                _smtpClient.EnableSsl = true;
            }

            MailAddress _from = new MailAddress(strfrom, nickName);
            MailAddress _to = new MailAddress(strto);
            MailMessage _mailMessage = new MailMessage(_from, _to);
            _mailMessage.Subject = subj;//主题
            _mailMessage.Body = bodys;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.Normal;//优先级
            _smtpClient.Send(_mailMessage);
        }
        #endregion
    }

    /// <summary>
    /// DES加密/解密类。
    /// </summary>
    public class DESEncrypt
    {

        #region ========加密========

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Encrypt(string Text)
        {
            return Encrypt(Text, "DTcms");
        }
        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Encrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.Default.GetBytes(Text);
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        #endregion

        #region ========解密========

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
        public static string Decrypt(string Text)
        {
            return Decrypt(Text, "DTcms");
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="Text"></param> 
        /// <param name="sKey"></param> 
        /// <returns></returns> 
        public static string Decrypt(string Text, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            int len;
            len = Text.Length / 2;
            byte[] inputByteArray = new byte[len];
            int x, i;
            for (x = 0; x < len; x++)
            {
                i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Encoding.Default.GetString(ms.ToArray());
        }

        #endregion

    }
}