using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.WebApi.ViewModels;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IUserLogic _userLogic;

        public BaseController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        public UserViewModel LoginUser
        {
            get
            {
                if (LoginUserId == 0) return null;
                return _userLogic.Get(LoginUserId).ToViewModel();
            }
        }
        public int LoginUserId
        {
            get
            {
                if (!HttpContext.Current.Request.Headers.AllKeys.Contains("LoginUserId")) return 0;
                return int.Parse(HttpContext.Current.Request.Headers["LoginUserId"]);
            }
        }
    }
}
