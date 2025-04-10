
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

        private static TimeSpan period = TimeSpan.FromSeconds(5);
        private readonly ILogger<ServerTimeNotification> logger;
        private readonly IHubContext<NotificationHub, INotifationClient> context;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(period);

            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                var dateTime = DateTime.Now;

                logger.LogInformation($"It's working {nameof(ServerTimeNotification)} {dateTime} ");

                await context.Clients.
                    User("90d4cb5d-72ae-434d-8dd0-e768709558de")
                    .ReceiveNotification($"Server Time = {dateTime}"); 
            }

        }
    }
}
