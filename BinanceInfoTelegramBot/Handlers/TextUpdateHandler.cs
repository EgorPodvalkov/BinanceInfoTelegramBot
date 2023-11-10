using Telegram.Bot;
using Telegram.Bot.Types;

namespace BinanceInfoTelegramBot.Handlers
{
    public class TextUpdateHandler : IHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly Update _update;

        public TextUpdateHandler(ITelegramBotClient botClient, Update update)
        {
            _botClient = botClient;
            _update = update;
        }

        public async Task Handle()
        {
            var text = _update.Message?.Text;

            if (text is null)
                return;

            var command = text.Split(' ').First().ToLower();
            switch (command)
            {
                case "/p2p":
                case "!p2p":
                    await P2pCommand(text);
                    break;
            }
        }

        private async Task P2pCommand(string text)
        {
            await _botClient.SendTextMessageAsync(_update.Message.Chat.Id, "bot не працює");
        }
    }
}
