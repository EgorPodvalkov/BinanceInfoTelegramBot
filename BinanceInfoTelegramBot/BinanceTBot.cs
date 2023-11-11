using BinanceInfoTelegramBot.Handlers;
using BinanceInfoTelegramBot.Settings;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BinanceInfoTelegramBot
{
    public class BinanceTBot
    {
        private readonly ITelegramBotClient _botClient;
        private readonly ReceiverOptions _receiverOptions;
        private readonly ILogger<TelegramBotService> _logger;

        /// <summary>
        /// Возвращает сконфигурированого бота
        /// </summary>
        public BinanceTBot(ILogger<TelegramBotService> logger)
        {
            var token = TelegramBotSettings.Token;
            _botClient = new TelegramBotClient(token);
            _receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new[]
                {
                    UpdateType.Message,
                },
                ThrowPendingUpdates = true,
            };
            _logger = logger;
        }

        /// <summary>
        /// Bot running
        /// </summary>
        public async Task Run()
        {
            using var cts = new CancellationTokenSource();

            _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token);

            var me = await _botClient.GetMeAsync();
            _logger.LogInformation($"{me.FirstName} запущен!");
        }

        private async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            try
            {
                switch (update.Type)
                {
                    case UpdateType.Message:
                        {
                            var textUpdateHandler = new TextUpdateHandler(botClient, update);
                            await textUpdateHandler.Handle();
                            return;
                        }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                if (TelegramBotSettings.LogChatID is null)
                    return;

                var message = string.Format("#Log #Error #InnerError \n{1} \n{0}", ex.StackTrace, update.Message?.Text ?? "No message");
                await _botClient.SendTextMessageAsync(TelegramBotSettings.LogChatID, message);
            }
        }

        private Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
        {
            var ErrorMessage = error switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => error.ToString()
            };

            _logger.LogError(ErrorMessage);
            return Task.CompletedTask;
        }

    }
}
