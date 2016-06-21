using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;
using Shinetech.PlanPoker.Logic.Tools;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    [RoutePrefix("api")]
    public class ProjectController : BaseController
    {
        private readonly IProjectLogic _projectLogic;

        public ProjectController(IUserLogic userLogic, IProjectLogic projectLogic) : base(userLogic)
        {
            _projectLogic = projectLogic;
        }

        [HttpGet]
        [Route("projects")]
        [PlanPokerAuthorize]
        public ProjectsViewModel GetProjects(int pageNumber, int pageCount, string queryText = "")
        {
            var result = new ProjectsViewModel
            {
                Pages = _projectLogic.GetPages(LoginUserId, pageNumber, pageCount, queryText),
                ProjectViewModels =
                    _projectLogic.GetProjectByUser(LoginUserId, pageNumber, pageCount, queryText)
                        .Select(x => new ProjectViewModel
                        {
                            Id = x.Id,
                            Name = x.Name,
                            OwnerViewModel = x.OwnerLogicModel.ToViewModel()
                        }).ToList()
            };

            return result;
        }

        [HttpDelete]
        [Route("project")]
        [PlanPokerAuthorize]
        public void DeleteProject(int projectId)
        {
            _projectLogic.Delete(projectId);
        }

        [HttpPost]
        [Route("project")]
        [PlanPokerAuthorize]
        public void CreateProject(ProjectViewModel project)
        {
            _projectLogic.Create(LoginUser.ToLogicModel(), project.ToLogicModel());
        }

        [HttpPut]
        [Route("project")]
        [PlanPokerAuthorize]
        public void EditProject(ProjectViewModel project)
        {
            _projectLogic.Edit(project.ToLogicModel());
        }

        [HttpGet]
        [Route("projectdecrypt")]
        public string DecryptProjectCode(string projectCode)
        {
            return TokenGenerator.DecodeToken(projectCode);
        }

        [HttpGet]
        [Route("projectencrypt")]
        public string EncryptProjectCode(string projectId)
        {
            return TokenGenerator.EncodeToken(projectId);
        }

        [HttpPost]
        [Route("inviteprojectemail")]
        [PlanPokerAuthorize]
        public bool SendEmail(SendEmailViewModel sendEmailViewModel)
        {
            return _projectLogic.SendEmail(sendEmailViewModel.ToLogicModel());
        }

    }
}