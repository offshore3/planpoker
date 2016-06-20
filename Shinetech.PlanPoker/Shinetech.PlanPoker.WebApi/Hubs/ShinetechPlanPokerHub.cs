using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace Shinetech.PlanPoker.WebApi.Hubs
{
    [HubName("ShinetechPlanPokerHub")]
    public class ShinetechPlanPokerHub : Hub
    {
        public void Subscribe(string projectId)
        {
            Groups.Add(Context.ConnectionId, HubHelper.GetProjectId(projectId));
        }

        public void Unsubscribe(string projectId)
        {
            Groups.Remove(Context.ConnectionId, HubHelper.GetProjectId(projectId));
        }
    }
}