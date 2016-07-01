using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Configuration;
using Castle.Core.Internal;
using Newtonsoft.Json;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.Logic.Tools;
using Shinetech.PlanPoker.LogicModel;
using Shinetech.PlanPoker.Repository.UnitOfWork;

namespace Shinetech.PlanPoker.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _userRepository = userRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public void Create(UserLogicModel model)
        {
            var userModel = model.ToModel();

            if (_userRepository.Query().Any(x => x.Email == model.Email))
            {
                throw new PlanPokerException("The email is already exist.");
            }

            if (model.Password != model.ComfirmPassword)
            {
                throw new PlanPokerException("Confirm is not match with password.");
            }

            userModel.Password = TokenGenerator.EncodeToken(model.Password);
            userModel.ExpiredTime = DateTime.Now;
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                _userRepository.Save(userModel);
                unitOfwork.Commit();
            }
        }

        public void Edit(UserLogicModel model)
        {
            var userModel = _userRepository.GetForUpdate(model.Id);
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.Name = model.Name;
                userModel.ImagePath = model.ImagePath;
                userModel.ExpiredTime = DateTime.Now;
                unitOfwork.Commit();
            }
        }

        public void EditPassword(UserLogicModel model)
        {
            var userModel = _userRepository.GetForUpdate(model.Id);
            if (model.ComfirmPassword != null && model.Password != model.ComfirmPassword)
            {
                throw new PlanPokerException("Confirm is not match with password.");
            }

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.Password = TokenGenerator.EncodeToken(model.Password);
                userModel.ExpiredTime = DateTime.Now;
                unitOfwork.Commit();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public UserLogicModel Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null) return null;
            return user.ToLogicModel();
        }

        public string Login(string email, string password)
        {
            var user = _userRepository.Query().FirstOrDefault(x => x.Email == email && x.Password == TokenGenerator.EncodeToken(password));
            var isLoginSuccess = user != null;

            return isLoginSuccess
                ? TokenGenerator.Generate(email, password, DateTime.MaxValue.ToString("yyyy-MM-dd hh:mm")) + ",," +
                  user.Id
                : string.Empty;
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            return _userRepository.Query()?.Select(x => new UserLogicModel
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.Password
            });
        }

        public bool CheckEmailExist(string email)
        {
            return _userRepository.Query().Any(x => x.Email == email);
        }

        public bool CheckToken(string email, string password)
        {
            bool isLoginUser;
            try
            {
                isLoginUser = _userRepository.Query().Any(x => x.Email == email && x.Password == TokenGenerator.EncodeToken(password));
            }
            catch (Exception)
            {
                isLoginUser = true;
            }
            return isLoginUser;
        }

        public UserLogicModel GetUserByEmail(string email)
        {
            var user= _userRepository.Query().FirstOrDefault(x => x.Email == email&& x.ExpiredTime>DateTime.Now )?.ToLogicModel();
            
            return user;
        }
        public bool SendForgetPassowrdEmail(SendEmailLogicModel model)
        {
            if (!_userRepository.Query().Any(x => x.Email == model.MailLogicModel.EmailTo)) return false;

            var userModel = _userRepository.Query().FirstOrDefault(x => x.Email == model.MailLogicModel.EmailTo);
            
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                if (userModel != null)
                {
                    userModel.ResetPasswordToken = TokenGenerator.EncodeToken(model.MailLogicModel.EmailTo + "&" + DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));
                    userModel.ExpiredTime = DateTime.Now.AddHours(1);
                    _userRepository.Save(userModel);
                }
                unitOfwork.Commit();
            }            

            var titletxt = model.MailContentLogicModel.MailTitle;
            var bodytxt = model.MailContentLogicModel.Content;

            bodytxt = bodytxt.Replace("{webname}", model.MailLogicModel.WebName);
            bodytxt = bodytxt.Replace("{weburl}", model.MailLogicModel.WebUrl);
            bodytxt = bodytxt.Replace("{webtel}", model.MailLogicModel.WebTel);
            if (userModel != null)
                bodytxt = bodytxt.Replace("{linkurl}", model.MailLogicModel.AbsUrl + "?code=" + userModel.ResetPasswordToken);

            try
            {
                SendEmailLogicModel.SendMail(model.MailLogicModel.EmailSmtp,
                    model.MailLogicModel.EmailSsl,
                    model.MailLogicModel.EmailUserName,
                    TokenGenerator.DecodeToken(model.MailLogicModel.EmailPassWord),
                    model.MailLogicModel.EmailNickName,
                    model.MailLogicModel.EmailFrom,
                    model.MailLogicModel.EmailTo,
                    titletxt, bodytxt);
            }
            catch
            {
                return false;
            }

            return true;
        }
        
        public string LoginWithLinkedIn(string code, string state)
        {
#if DEBUG
            GlobalProxySelection.Select = GlobalProxySelection.GetEmptyWebProxy();
#endif
            var linkedInLoginRedirectUrl = WebConfigurationManager.AppSettings["LinkedInLoginRedirectUrl"];
            var linkedInLoginAppSecret = WebConfigurationManager.AppSettings["LinkedInLoginAppSecret"];
            var linkedInLoginAppId = WebConfigurationManager.AppSettings["LinkedInLoginAppId"];
            var linkedInGetAccountDetailUrl = WebConfigurationManager.AppSettings["LinkedInGetAccountDetailUrl"];
            var linkedInGetAccessTokenAddress = WebConfigurationManager.AppSettings["LinkedInGetAccessTokenAddress"];
            var webPath = WebConfigurationManager.AppSettings["WebPath"];
            string authorization;

            using (var client = new HttpClient())
            {
                var values = new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"code", code},
                    {"redirect_uri", linkedInLoginRedirectUrl},
                    {"client_id", linkedInLoginAppId},
                    {"client_secret", linkedInLoginAppSecret}
                };

                var body = new FormUrlEncodedContent(values);
                var str = client.PostAsync(linkedInGetAccessTokenAddress, body).Result;
                var content = str.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<LinkedInAuthResult>(content);

                if (result.AccessToken.IsNullOrEmpty())
                {
                    throw new PlanPokerException("Get access token failed - " + content);
                }

                using (var profileClient = GetLinkedInHttpClient(result.AccessToken))
                {
                    var httpResponse = profileClient
                        .GetAsync(linkedInGetAccountDetailUrl).Result;
                    var profileContent = httpResponse.Content.ReadAsStringAsync().Result;
                    var profile = JsonConvert.DeserializeObject<LinkedInLogicModel>(profileContent);
                    var user = _userRepository.Query().FirstOrDefault(x => x.Email == profile.Email);
                    if (user != null)
                    {
                        authorization = Login(user.Email, TokenGenerator.DecodeToken(user.Password));
                    }
                    else {
                        var userLogicModel = new UserLogicModel
                        {
                            Email = profile.Email,
                            Name = profile.FirstName + " " + profile.LastName,
                            Password = "123456",
                            ComfirmPassword = "123456",
                            ExpiredTime = DateTime.Now,
                            ImagePath = profile.Picture
                        };
                        Create(userLogicModel);
                        authorization = Login(userLogicModel.Email, userLogicModel.Password);
                    }
                }
            }

            return webPath + "#/login?authorization=" + authorization;
        }

        private static HttpClient GetLinkedInHttpClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-li-format", "json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        private static HttpClient GetFacebookHttpClient(string token)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.TryAddWithoutValidation("x-li-format", "json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return client;
        }

        public string LoginWithFacebook(string code, string state)
        {
#if DEBUG
            GlobalProxySelection.Select = new WebProxy("127.0.0.1:1080");
#endif
            var facebookLoginRedirectUrl = WebConfigurationManager.AppSettings["FacebookLoginRedirectUrl"];
            var facebookLoginAppSecret = WebConfigurationManager.AppSettings["FacebookLoginAppSecret"];
            var facebookLoginAppId = WebConfigurationManager.AppSettings["FacebookLoginAppId"];
            var facebookGetAccountDetailUrl = WebConfigurationManager.AppSettings["FacebookGetAccountDetailUrl"];
            var facebookGetAccessTokenAddress = WebConfigurationManager.AppSettings["FacebookGetAccessTokenAddress"];
            var webPath = WebConfigurationManager.AppSettings["WebPath"];
            string authorization;
            

            using (var client = new HttpClient())
            {
                var getTokenUrl = $"{facebookGetAccessTokenAddress}client_id={facebookLoginAppId}&" +
                                  $"client_secret={facebookLoginAppSecret}&redirect_uri={facebookLoginRedirectUrl}&code={code}";
                var str = client.GetAsync(getTokenUrl).Result;
                var content = str.Content.ReadAsStringAsync().Result;

                var result = JsonConvert.DeserializeObject<FacebookAuthResult>(content);

                if (result.AccessToken.IsNullOrEmpty())
                {
                    throw new PlanPokerException("Get access token failed - " + content);
                }

                using (var profileClient = GetFacebookHttpClient(result.AccessToken))
                {
                    var facebookUserInforequest = string.Format(facebookGetAccountDetailUrl, result.AccessToken);
                    var httpResponse = profileClient
                        .GetAsync(facebookUserInforequest).Result;
                    var profileContent = httpResponse.Content.ReadAsStringAsync().Result;
                    var profile = JsonConvert.DeserializeObject<dynamic>(profileContent);
                    string email = profile.email.ToString();
                    var user = _userRepository.Query().FirstOrDefault(x => x.Email == email);
                    if (user != null)
                    {
                        authorization = Login(user.Email, TokenGenerator.DecodeToken(user.Password));
                    }
                    else {
                        var userLogicModel = new UserLogicModel
                        {
                            Email = profile.email.ToString(),
                            Name = profile.first_name + " " + profile.last_name,
                            Password = "123456",
                            ComfirmPassword = "123456",
                            ExpiredTime = DateTime.Now,
                            ImagePath = profile.picture.data.url.ToString()
                        };
                        Create(userLogicModel);
                        authorization = Login(userLogicModel.Email, userLogicModel.Password);
                    }
                }
            }

            return webPath + "#/login?authorization=" + authorization;
        }
    }
}