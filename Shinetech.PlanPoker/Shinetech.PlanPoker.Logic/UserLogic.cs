using System;
using System.Collections.Generic;
using System.Linq;
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

            if (_userRepository.Query().Any(x => x.Email == model.Email)) return;

            if (model.Password != model.ComfirmPassword) return;

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
            if (model.Password != model.ComfirmPassword) return;

            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.Password = model.Password;
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
            return _userRepository.Get(id)?.ToLogicModel();
        }

        public string Login(string email, string password)
        {
            var user = _userRepository.Query().FirstOrDefault(x => x.Email == email && x.Password== TokenGenerator.EncodeToken(password));
            var isLoginSuccess = user != null;

            return isLoginSuccess
                ? TokenGenerator.Generate(email, password, DateTime.MaxValue.ToString("yyyy-MM-dd hh:mm")) + "&" +
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
            bool isLoginUser = false;
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
            var deCodeEmail = TokenGenerator.DecodeToken(email);
            return _userRepository.Query().FirstOrDefault(x => x.Email == deCodeEmail).ToLogicModel();
        }
        public bool SendEmail(SendEmailLogicModel model)
        {
            if (!_userRepository.Query().Any(x => x.Email == model.MailLogicModel.EmailTo)) return false;

            var titletxt = model.MailContentLogicModel.MailTitle;
            var bodytxt = model.MailContentLogicModel.Content;

            bodytxt = bodytxt.Replace("{webname}", model.MailLogicModel.WebName);
            bodytxt = bodytxt.Replace("{weburl}", model.MailLogicModel.WebUrl);
            bodytxt = bodytxt.Replace("{webtel}", model.MailLogicModel.WebTel);
            bodytxt = bodytxt.Replace("{linkurl}", model.MailLogicModel.AbsUrl + "?code=" + TokenGenerator.EncodeToken(model.MailLogicModel.EmailCode));

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
    }
}