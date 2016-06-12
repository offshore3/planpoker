using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
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
        public ProjectsViewModel GetProjects(int pageNumber, int pageCount, string queryText = "")
        {
            var result = new ProjectsViewModel
            {
                Pages = _projectLogic.GetPages(LoginUserId,pageNumber, pageCount, queryText),
                ProjectViewModels = _projectLogic.GetByUser(LoginUserId, pageNumber, pageCount, queryText).Select(x => new ProjectViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    OwnerViewModel = x.OwnerLogicModel.ToViewModel(),
                    //Participates = x.Participates?.Select(y => y.ToViewModel())
                }).ToList()
            };

            return result;
        }

    }
}
