using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }
        [HttpGet]
        [Route("api/users")]
        public List<UserViewModel> GetUsers()
        {
            return _userLogic.GetUsers().Select(x=>new UserViewModel
            {
                Email = x.Email,Name = x.Name,Password = x.Password
            }).ToList();
        }
    }
}