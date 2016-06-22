using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.Logic.Tools;
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
                var userlogicModel = _userLogic.Get(LoginUserId);
                userlogicModel.Password = TokenGenerator.DecodeToken(userlogicModel.Password);
                return userlogicModel.ToViewModel();
            }
        }

        public int LoginUserId
        {
            get
            {
                try
                {
                    if (!HttpContext.Current.Request.Headers.AllKeys.Contains("LoginUserId")) return 0;
                    return int.Parse(HttpContext.Current.Request.Headers["LoginUserId"]);
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }
    }
}