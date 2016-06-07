using System.Collections.Generic;
using System.Linq;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.IRepository;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void Create(UserLogicModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(UserLogicModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public UserLogicModel Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserLogicModel> GetAll()
        {
            return _userRepository.Query().Select(x => new UserLogicModel
            {
                Email = x.Email,
                Name = x.Name,
                Password = x.Password
            }).ToList();
        }
    }
}
