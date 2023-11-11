using BinanceInfoTelegramBot.Settings;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace BinanceInfoTelegramBot.Handlers
{
    public class TextUpdateHandler : IHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly Update _update;

        private long _chatId => _update.Message?.Chat.Id ?? throw new Exception("Update message is null.");
        private string _senderName
            => _update.Message is null ? "no name"
            : _update.Message.From is null ? "security"
            : "@" + _update.Message.From.Username ?? _update.Message.From.FirstName + (_update.Message.From.LastName ?? "");

        public TextUpdateHandler(ITelegramBotClient botClient, Update update)
        {
            _botClient = botClient;
            _update = update;
        }

        public async Task Handle()
        {

            if (_update.Message is null || _update.Message.Text is null)
                return;

            var text = _update.Message.Text;

            var command = text.Split(' ', '@').First().ToLower();
            switch (command)
            {
                case "/p2p":
                case "/p2pinfo":
                case "!p2p":
                case "!p2pinfo":
                    await P2pInfoCommand(text);
                    return;

                case "/p2pbuy":
                case "!p2pbuy":
                    await P2pBuyCommand(text);
                    return;

                case "/p2psell":
                case "!p2psell":
                    await P2pSellCommand(text);
                    return;

                case "/p2pbotproblem":
                case "!p2pbotproblem":
                    await P2pProblemCommand(text);
                    return;
            }
        }

        private async Task P2pInfoCommand(string text) => await
            _botClient.SendTextMessageAsync(_chatId, TelegramBotSettings.InfoCommandMessage, parseMode: ParseMode.Markdown);

        private async Task P2pBuyCommand(string text)
        {

        }

        private async Task P2pSellCommand(string text)
        {

        }

        private async Task P2pProblemCommand(string text)
        {
            if (TelegramBotSettings.LogChatID is null)
                return;
            var message = string.Format("#botProblem \n {0}: {1}", _senderName, text);
            var success = await _botClient.SendTextMessageAsync(TelegramBotSettings.LogChatID, message, parseMode: ParseMode.Markdown) is not null;

            if (success && TelegramBotSettings.ProblemCommandMessage is not null)
                await _botClient.SendTextMessageAsync(_chatId, TelegramBotSettings.ProblemCommandMessage, parseMode: ParseMode.Markdown);
        }
    }
}
