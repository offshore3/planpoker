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
        string Login(int userId);
        IEnumerable<UserLogicModel> GetAll();
        bool CheckEmailExist(string email);
        bool CheckToken(string email, string password);
        UserLogicModel GetUserByEmail(string email);
        bool SendForgetPassowrdEmail(SendEmailLogicModel model);

        UserLogicModel GetUserByOpenId(UserLogicModel model);
        void UpdateUserEmail(UserLogicModel toLogicModel, int loginUserId);
    }
}