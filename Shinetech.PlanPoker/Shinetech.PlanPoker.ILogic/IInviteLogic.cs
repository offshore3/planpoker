using System.Collections.Generic;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IInviteLogic
    {
        void Create(InviteLogicModel model);
        void Create(int projectId,string email);
        void Delete(int id);
        InviteLogicModel Get(int id);
        InviteLogicModel Get(int projectId, string email);
        IEnumerable<InviteLogicModel> GetAll();
        IEnumerable<ParticipatesLogicModel> GetParticipatesByProjectId(int projectId);
        bool CheckInviteExist(int projectId, string email);
        void Edit(string enCodeProjectId, string email);
    }
}