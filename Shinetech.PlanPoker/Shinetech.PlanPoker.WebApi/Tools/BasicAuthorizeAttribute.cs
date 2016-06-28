using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using Shinetech.PlanPoker.ILogic;
using Shinetech.PlanPoker.Logic.Tools;
using Shinetech.PlanPoker.WebApi.Installer;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public class PlanPokerAuthorizeAttribute : AuthorizeAttribute
    {
        private IUserLogic _userLogic;

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var token = actionContext.Request.Headers.Authorization;
            if (token == null)
            {
                throw new Exception("Unauthenticated");
            }
            if (CheckTokenIsValidAndManageCache(token.ToString()))
            {
                throw new Exception("Unauthenticated");
            }
        }

        private bool CheckTokenIsValidAndManageCache(string tokenStr)
        {
            _userLogic = WindsorBootstrapper.Container.Resolve<IUserLogic>();
            var result = false;
            var credentialstring = TokenGenerator.DecodeToken(tokenStr);
            var userId = 0;
            int.TryParse(credentialstring, out userId);
            var user = _userLogic.Get(userId);
            if (userId==0 || user==null)
            {
                result = true;
            }
            return result;
        }
    }
}