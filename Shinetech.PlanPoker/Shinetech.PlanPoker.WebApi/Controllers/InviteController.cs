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
        private readonly IUserLogic _userLogic;

        public InviteController(IUserLogic userLogic, IInviteLogic inviteLogic) : base(userLogic)
        {
            _inviteLogic = inviteLogic;
            _userLogic = userLogic;
        }

        [HttpGet]
        [Route("participates")]
        [PlanPokerAuthorize]
        public List<ParticipatesViewModel> GetParticipatesByProjectId(int projectId)
        {
            return _inviteLogic.GetParticipatesByProjectId(projectId).Select(x => x.ToViewModel()).ToList();
        }

        [HttpDelete]
        [Route("participates")]
        [PlanPokerAuthorize]
        public void DeleteParticipates(int participateId)
        {
            _inviteLogic.Delete(participateId);
        }

        [HttpPost]
        [Route("participate")]
        [PlanPokerAuthorize]
        public void CreateInvite(InviteParticipateViewModel inviteParticipateViewModel)
        {
            _inviteLogic.Create(inviteParticipateViewModel.ProjectId, inviteParticipateViewModel.Email);
        }

        [HttpPut]
        [Route("participate")]
        public void UpdateInvite(InviteParticipateViewModel inviteParticipateViewModel)
        {
            _inviteLogic.Edit(inviteParticipateViewModel.EndCodeProjectId, inviteParticipateViewModel.Email);
        }

        [HttpGet]
        [Route("participate")]
        [PlanPokerAuthorize]
        public InviteViewModel Get(int projectId,string email)
        {
            return _inviteLogic.Get(projectId, email).ToViewModel();
        }
    }
}