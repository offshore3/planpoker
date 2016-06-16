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

        [HttpPost]
        [Route("participate")]
        [BasicAuthorize]
        public void Create(InviteParticipateViewModel inviteParticipateViewModel)
        {
            _inviteLogic.Create(inviteParticipateViewModel.ProjectId, inviteParticipateViewModel.Email);
        }

        [HttpGet]
        [Route("get-invite-by-projectid-email")]
        [BasicAuthorize]
        public InviteViewModel Get(int projectId,string email)
        {
            return _inviteLogic.Get(projectId, email).ToViewModel();
        }

        //[HttpPost]
        //[Route("invite-participate")]
        //[BasicAuthorize]
        //public bool InviteParticipates(InviteParticipateViewModel inviteParticipateViewModel)
        //{
        //    var isSendEmailSuccess = true;
        //    if (!_inviteLogic.CheckInviteExist(inviteParticipateViewModel.ProjectId, inviteParticipateViewModel.Email))
        //    {
        //        _inviteLogic.Create(inviteParticipateViewModel.ProjectId, inviteParticipateViewModel.Email);
        //    }
        //    if (!_userLogic.CheckEmailExist(inviteParticipateViewModel.Email))
        //    {
        //        //TODO add send email.
        //    }
        //    return isSendEmailSuccess;
        //}
    }
}