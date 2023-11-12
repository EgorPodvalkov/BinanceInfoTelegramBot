using BinanceInfoTelegramBot.Classes;

namespace BinanceInfoTelegramBot.AppSettings
{
    public static class TGMessageTemplates
    {
        private static IConfigurationSection _config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("TGMessageTemplates");

        /// <summary> Format: {Fiat} - your Fiat from TGBotSettings </summary>
        private static string? MyBaseFormat(string? template)
        {
            if (string.IsNullOrEmpty(template))
                return null;
            return template
                .Replace("{Fiat}", TGBotSettings.Fiat);
        }

        public static string InfoCommandMessage => MyBaseFormat(InfoCommandMessageTemplate) ?? throw new Exception("No info message.");
        private static string InfoCommandMessageTemplate => _config["InfoCommandMessage"]
            ?? "Use /p2pBuy or /p2pSell comands to get actual p2p offers.\n"
            + "You can use this commands with unnessesary parametrs crypto name, trans amount and pay types. Pay attantion for order of params.\n"
            + "Command with using all parametrs example: \n"
            + "\"`/p2pbuy usdt 500 monobank privatbank`\", \n"
            + "'usdt' - name of crypto to buy, '500' - amount of {Fiat} you want to sell and 'monobank privatbank' - pay type names you wand to use.";

        public static string? ProblemCommandMessage => MyBaseFormat(ProblemCommandMessageTemplate);
        private static string? ProblemCommandMessageTemplate => _config["ProblemCommandMessage"];

        /// <summary> Format:  <br/>
        /// {Price} - order price in your fiat, <br/>
        /// {Crypto} - requested crypto, <br/>
        /// {FiatSymbol} - fiat symbol,  <br/>
        /// {MaxTransAmount} - maximum single transaction amount in your fiat,  <br/>
        /// {MinTransAmount} - minimal single transaction amount in your fiat,  <br/>
        /// {PayTimeLimit} - order pay time limit,  <br/>
        /// {PayTypes} - order pay time limit,  <br/>
        /// {CreatorNickName} - nickname of creator,  <br/>
        /// {Fiat} - your Fiat from TGBotSettings 
        /// </summary>
        private static string MyResponseFormat(string template, Order order)
            => template
                .Replace("{Price}", order.OrderDetail.Price.ToString())
                .Replace("{Crypto}", order.OrderDetail.Crypto)
                .Replace("{FiatSymbol}", order.OrderDetail.FiatSymbol)
                .Replace("{MaxTransAmount}", order.OrderDetail.MaxSingleTransAmount.ToString())
                .Replace("{MinTransAmount}", order.OrderDetail.MinSingleTransAmount.ToString())
                .Replace("{PayTimeLimit}", order.OrderDetail.PayTimeLimit.ToString())
                .Replace("{PayTypes}", string.Join(", ", order.OrderDetail.PayTypes))
                .Replace("{CreatorNickName}", order.OrderCreator.NickName.MarkdownShield())
                .Replace("{Fiat}", TGBotSettings.Fiat)
                + "\n";

        /// <summary> Format: {Error} - binance Error, {Fiat} - your Fiat from TGBotSettings </summary>
        private static string MyErrorResponseFormat(string template, P2pSearchResponse response)
            => template
                .Replace("{Error}", (response.Message ?? "Error") + response.MessageDetail is not null ? ": " + response.MessageDetail : string.Empty)
                .Replace("{Fiat}", TGBotSettings.Fiat);

        public static string GetSellOrderMessage(P2pSearchResponse response)
        {
            if (!response.Success)
                return MyErrorResponseFormat(SellOrderErrorTemplate, response);

            if (response.Orders is null || !response.Orders.Any())
                return NoFilteredItemsSellOrderErrorMessage;

            var result = "";
            foreach (var data in response.Orders)
            {
                result += MyResponseFormat(SellOrderSuccessTemplate, data);
            }
            return result;
        }
        private static string SellOrderSuccessTemplate => _config["SellOrderSuccess"] ?? "Price: {Price}";
        private static string SellOrderErrorTemplate => _config["SellOrderError"] ?? "{Error}.";
        private static string NoFilteredItemsSellOrderErrorMessage => _config["NoFilteredItemsSellOrderError"]
            ?? "No items with such filter. :(";

        public static string GetBuyOrderMessage(P2pSearchResponse response)
        {
            if (!response.Success)
                return MyErrorResponseFormat(BuyOrderErrorTemplate, response);

            if (response.Orders is null || !response.Orders.Any())
                return NoFilteredItemsBuyOrderErrorMessage;

            var result = "";
            foreach (var data in response.Orders)
            {
                result += MyResponseFormat(BuyOrderSuccessTemplate, data);
            }
            return result;
        }
        private static string BuyOrderSuccessTemplate => _config["BuyOrderSuccess"] ?? "Price: {Price}";
        private static string BuyOrderErrorTemplate => _config["BuyOrderError"] ?? "{Error}.";
        private static string NoFilteredItemsBuyOrderErrorMessage => _config["NoFilteredItemsBuyOrderError"]
            ?? "No items with such filter. :(";

        private static string MarkdownShield(this string text)
            => text
            .Replace("_", "")
            .Replace("*", "")
            .Replace("~", "")
            .Replace("|", "")
            .Replace("`", "");
    }
}
