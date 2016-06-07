using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Shinetech.PlanPoker.Logic.Tools;

namespace Shinetech.PlanPoker.WebApi.Tools
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {

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
            bool result = false;
            var credentialstring = TokenGenerator.DecodeToken(tokenStr);

            var datas = credentialstring.Split('&');
            string userEmail = datas[0];
            string password = datas[1];
            if (string.IsNullOrEmpty(userEmail)|| string.IsNullOrEmpty(password))
            {
                result = true;
            }
            return result;
        }
    }
}