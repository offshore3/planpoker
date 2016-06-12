using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.Tools;
using Shinetech.PlanPoker.WebApi.ViewModels;

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
        [BasicAuthorize]
        public ProjectsViewModel GetProjects(int pageNumber, int pageCount, string queryText = "")
        {
            var result = new ProjectsViewModel
            {
                Pages = _projectLogic.GetPages(LoginUserId,pageNumber, pageCount, queryText),
                ProjectViewModels = _projectLogic.GetProjectByUser(LoginUserId, pageNumber, pageCount, queryText).Select(x => new ProjectViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerViewModel = x.OwnerLogicModel.ToViewModel(),
                    //Participates = x.Participates?.Select(y => y.ToViewModel())
                }).ToList()
            };

            return result;
        }

        [HttpDelete]
        [Route("project")]
        [BasicAuthorize]
        public void DeleteProject(int projectId)
        {
            _projectLogic.Delete(projectId);
        }

        [HttpPost]
        [Route("project")]
        [BasicAuthorize]
        public void CreateProject(ProjectViewModel project)
        {
            _projectLogic.Create(LoginUser.ToLogicModel(),project.ToLogicModel());
        }

        [HttpPut]
        [Route("project")]
        [BasicAuthorize]
        public void EditProject(ProjectViewModel project)
        {
            _projectLogic.Edit(project.ToLogicModel());
        }

    }
}
