using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SignalR.api.Classes
{
    [Authorize]
    public class NotificationHub : Hub<INotifationClient>
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.Client(Context.ConnectionId).ReceiveNotification(
                   $"Connection working fine for {Context?.User?.Identity?.Name}");

            await base.OnConnectedAsync();
        }

    }

    public interface INotifationClient
    {
        Task ReceiveNotification(string message);
    }
}
