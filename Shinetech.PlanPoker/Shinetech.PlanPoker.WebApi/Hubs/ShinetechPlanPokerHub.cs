using System.Collections.Generic;
using System.Threading.Tasks;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.Lifestyle;
using Castle.MicroKernel.ModelBuilder;
using Castle.Windsor;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using Shinetech.PlanPoker.WebApi.Installer;

namespace Shinetech.PlanPoker.WebApi.Hubs
{
    [HubName("ShinetechPlanPokerHub")]
    public class ShinetechPlanPokerHub : Hub
    {
        public Task Join(string projectId)
        {
            return Groups.Add(Context.ConnectionId, projectId);
        }
    }
}