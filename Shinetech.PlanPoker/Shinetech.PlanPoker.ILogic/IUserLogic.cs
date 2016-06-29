using System.Collections.Generic;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IUserLogic
    {
        void Create(UserLogicModel model);
        void Edit(UserLogicModel model);
        void EditPassword(UserLogicModel model);
        void Delete(int id);
        UserLogicModel Get(int id);
        string Login(string email, string password);
        IEnumerable<UserLogicModel> GetAll();
        bool CheckEmailExist(string email);
        bool CheckToken(string email, string password);
        UserLogicModel GetUserByEmail(string email);
        bool SendForgetPassowrdEmail(SendEmailLogicModel model);
        string LoginWithLinkedIn(string code, string state);
    }
}