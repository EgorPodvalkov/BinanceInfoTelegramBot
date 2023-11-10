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

            var bot = new BinanceTBot(_logger);
            await bot.Run();
        }

        public async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TelegramBotService stops at: {time}", DateTimeOffset.Now);

        }
    }
}