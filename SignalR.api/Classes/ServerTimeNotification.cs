
using Microsoft.AspNetCore.SignalR;

namespace SignalR.api.Classes
{
    public class ServerTimeNotification : BackgroundService
    {
        public ServerTimeNotification(ILogger<ServerTimeNotification> logger, IHubContext<NotificationHub, INotifationClient> context)
        {
            this.logger = logger;
            this.context = context;
        }

        private static TimeSpan period = TimeSpan.FromSeconds(10);
        private readonly ILogger<ServerTimeNotification> logger;
        private readonly IHubContext<NotificationHub, INotifationClient> context;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(period);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var dateTime = DateTime.Now;

                logger.LogInformation($"It's working {nameof(ServerTimeNotification)} {dateTime} ");

                await context.Clients.All.ReceiveNotification($"Server Time = {dateTime}"); 
            }

        }
    }
}
