namespace BinanceInfoTelegramBot
{
    public class TelegramBotService : IHostedService
    {
        private readonly ILogger<TelegramBotService> _logger;

        public TelegramBotService(ILogger<TelegramBotService> logger)
        {
            _logger = logger;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramBotService starts at: {time}", DateTimeOffset.Now);
            await Task.Delay(1000, stoppingToken);
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramBotService stops at: {time}", DateTimeOffset.Now);

        }
    }
}