using System.Collections.Generic;
using System.Linq;
using Shinetech.PlanPoker.Data.Models;

namespace Shinetech.PlanPoker.LogicModel
{
    public class ProjectLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserLogicModel OwnerLogicModel { get; set; }
        public IEnumerable<UserLogicModel> Participates { get; set; }
    }
}
