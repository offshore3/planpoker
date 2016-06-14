using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class InviteController : BaseController
    {
        private readonly IInviteLogic _inviteLogic;
        public InviteController(IUserLogic userLogic, IInviteLogic inviteLogic) : base(userLogic)
        {
            _inviteLogic = inviteLogic;
        }

        [HttpGet]
        [Route("participates")]
        [BasicAuthorize]
        public List<ParticipatesViewModel> GetParticipatesByProjectId(int projectId)
        {
            return _inviteLogic.GetParticipatesByProjectId(projectId).Select(x => x.ToViewModel()).ToList();
        }

        [HttpDelete]
        [Route("participates")]
        [BasicAuthorize]
        public void DeleteParticipates(int participateId)
        {
            _inviteLogic.Delete(participateId);
        }
    }
}
