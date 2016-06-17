using Microsoft.AspNet.SignalR;

namespace Shinetech.PlanPoker.WebApi.Hubs
{
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