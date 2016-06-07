using System.Collections.Generic;

namespace Shinetech.PlanPoker.LogicModel
{
    public class ProjectLogicModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserLogicModel OwnerLogicModel { get; set; }
        public IEnumerable<UserLogicModel> ParticipatesDto { get; set; }
    }
}
