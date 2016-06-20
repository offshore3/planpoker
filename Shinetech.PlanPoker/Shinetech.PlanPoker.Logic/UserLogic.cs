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

                unitOfwork.Commit();
            }
        }

        public void EditPassword(UserLogicModel model)
        {
            var userModel = _userRepository.GetForUpdate(model.Id);
            using (var unitOfwork = _unitOfWorkFactory.GetCurrentUnitOfWork())
            {
                userModel.Password = model.Password;

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
            var user = _userRepository.Query().FirstOrDefault(x => x.Email == email && password == x.Password);
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
            var isLoginUser = false;
            try
            {
                isLoginUser = _userRepository.Query().Any(x => x.Email == email && x.Password == password);
            }
            catch (Exception ex)
            {
                isLoginUser = false;
            }
            return isLoginUser;
        }

        public UserLogicModel GetUserByEmail(string email)
        {
            var deCodeEmail = TokenGenerator.DecodeToken(email);
            return _userRepository.Query().FirstOrDefault(x => x.Email == deCodeEmail).ToLogicModel();
        }
    }
}