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
        public void Subscribe(string customerId)
        {
            Groups.Add(Context.ConnectionId, customerId);
        }

        public void Unsubscribe(string customerId)
        {
            Groups.Remove(Context.ConnectionId, customerId);
        }
    }
}