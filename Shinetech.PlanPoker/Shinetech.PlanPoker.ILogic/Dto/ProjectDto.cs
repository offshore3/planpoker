using System.Collections.Generic;

namespace Shinetech.PlanPoker.ILogic.Dto
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserDto OwnerDto { get; set; }
        public IEnumerable<UserDto> ParticipatesDto { get; set; }
    }
}
