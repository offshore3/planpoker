using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Shinetech.PlanPoker.ILogic;

namespace Shinetech.PlanPoker.WebApi.Controllers
{
    public abstract class ApiControllerWithHub<THub> : BaseController
        where THub : IHub
    {
        Lazy<IHubContext> hub = new Lazy<IHubContext>(
            () => GlobalHost.ConnectionManager.GetHubContext<THub>()
        );

        public ApiControllerWithHub(IUserLogic userLogic) : base(userLogic)
        {
        }

        protected IHubContext Hub
        {
            get { return hub.Value; }
        }
    }
}
