using System.Collections.Generic;
using Shinetech.PlanPoker.ILogic.Dto;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IUserLogic
    {
        void Create(UserDto model);
        void Edit(UserDto model);
        void Delete(int id);
        UserDto Get(int id);
        int Login(string email, string password);
        IEnumerable<UserDto> GetAll();
    }
}