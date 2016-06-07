using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IUserLogic
    {
        List<UserLogicModel> GetUsers();
    }
}
