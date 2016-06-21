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

            var datas = credentialstring.Split('&');
            var userEmail = datas[0];
            var password = datas[1];
            var isUserExist = _userLogic.CheckToken(userEmail, password);
            if (string.IsNullOrEmpty(userEmail) || string.IsNullOrEmpty(password) || !isUserExist)
            {
                result = true;
            }
            return result;
        }
    }
}