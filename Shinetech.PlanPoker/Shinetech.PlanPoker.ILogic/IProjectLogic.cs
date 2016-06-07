using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shinetech.PlanPoker.ILogic.Dto;

namespace Shinetech.PlanPoker.ILogic
{
    public interface IProjectLogic
    {
        void Create(ProjectDto model);
        void Edit(ProjectDto model);
        void Delete(int id);
        ProjectDto Get(int id);
        IEnumerable<ProjectDto> GetAll();
    }
}
