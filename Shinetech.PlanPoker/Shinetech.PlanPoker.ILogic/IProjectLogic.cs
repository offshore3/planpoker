using System.Collections.Generic;
using Shinetech.PlanPoker.LogicModel;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        void Create(UserLogicModel user,ProjectLogicModel model);
        void Edit(ProjectLogicModel model);
        void Delete(int id);
        ProjectLogicModel Get(int id);
        IEnumerable<ProjectLogicModel> GetAll();
        IEnumerable<ProjectLogicModel> GetProjectByUser(int userId, int pageNumber, int pageCount, string queryText);
        int GetPages(int userId, int pageNumber, int pageCount, string queryText);
        bool SendInviteProjectEmail(SendEmailLogicModel model);
    }
}
