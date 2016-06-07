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

        public List<UserLogicModel> GetUsers()
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
