using System.Collections.Generic;
using Shinetech.PlanPoker.ILogic.Dto;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IInviteLogic
    {
        void Create(InviteDto model);
        void Delete(int id);
        InviteDto Get(int id);
        IEnumerable<InviteDto> GetAll();
    }
}