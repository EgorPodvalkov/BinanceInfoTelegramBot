namespace BinanceInfoTelegramBot.Settings
{
    public static class TelegramBotSettings
    {
        private static IConfigurationSection _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("TelegramBotSettings");

        public static string Token => _config["Token"];
        public static long? LogChatID
        {
            get
            {
                if (long.TryParse(_config["LogChatId"], out var result))
                    return result;
                else
                    return null;
            }
        }
        public static string InfoCommandMessage => string.Format(_config["InfoCommandMessage"], Fiat)
            ?? "Use /p2pBuy or /p2pSell comands to get actual p2p offers.\n"
            + "You can use this commands with unnessesary parametrs crypto name, trans amount and pay types. Pay attantion for order of params.\n"
            + "Command with using all parametrs example: \n"
            + "\"`/p2pbuy usdt 500 monobank privatbank`\", \n"
            + $"'usdt' - name of crypto to buy, '500' - amount of {Fiat} you want to sell and 'monobank privatbank' - pay type names you wand to use.";
        public static string Fiat => _config["Fiat"] ?? "UAH";
        public static string? ProblemCommandMessage => _config["ProblemCommandMessage"];
    }
}
