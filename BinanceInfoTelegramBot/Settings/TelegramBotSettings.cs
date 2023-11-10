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
    }
}
