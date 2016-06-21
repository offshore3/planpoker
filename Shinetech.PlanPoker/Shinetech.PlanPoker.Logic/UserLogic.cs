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
            user.Password = TokenGenerator.DecodeToken(user.Password);
            return user.ToLogicModel();
        }

        public string Login(string email, string password)
        {
            var user = _userRepository.Query().FirstOrDefault(x => x.Email == email && x.Password == TokenGenerator.EncodeToken(password));
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
            var user= _userRepository.Query().FirstOrDefault(x => x.Email == email).ToLogicModel();

            if (DateTime.Now> user.ExpiredTime)
            {
                throw new PlanPokerException("The email is already exist.");
            }
            return user;
        }
        public bool SendForgetPassowrdEmail(SendEmailLogicModel model)
        {
            if (!_userRepository.Query().Any(x => x.Email == model.MailLogicModel.EmailTo)) return false;

            var userModel = _userRepository.Query().FirstOrDefault(x => x.Email == model.MailLogicModel.EmailTo);
            
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.ResetPasswordToken = TokenGenerator.EncodeToken(model.MailLogicModel.EmailTo + "&" + DateTime.UtcNow.ToString());
                userModel.ExpiredTime = DateTime.Now.AddHours(1);
                _userRepository.Save(userModel);
                unitOfwork.Commit();
            }            

            var titletxt = model.MailContentLogicModel.MailTitle;
            var bodytxt = model.MailContentLogicModel.Content;

            bodytxt = bodytxt.Replace("{webname}", model.MailLogicModel.WebName);
            bodytxt = bodytxt.Replace("{weburl}", model.MailLogicModel.WebUrl);
            bodytxt = bodytxt.Replace("{webtel}", model.MailLogicModel.WebTel);
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
    }
}