namespace BinanceInfoTelegramBot.AppSettings
{
    public static class TGBotSettings
    {
        private static IConfigurationSection _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("TGBotSettings");

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
        public static string Fiat => _config["Fiat"] ?? "UAH";
    }
}
