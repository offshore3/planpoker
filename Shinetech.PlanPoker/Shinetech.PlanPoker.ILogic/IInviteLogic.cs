using System.Collections.Generic;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IInviteLogic
    {
        void Create(InviteLogicModel model);
        void Delete(int id);
        InviteLogicModel Get(int id);
        IEnumerable<InviteLogicModel> GetAll();
        IEnumerable<ParticipatesLogicModel> GetParticipatesByProjectId(int projectId);
    }
}