using BinanceInfoTelegramBot.Settings;
using System.Text.Json.Serialization;

namespace BinanceInfoTelegramBot.Classes
{
    public class P2pSearchRequest
    {
        /// <summary> Number of page </summary>
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        /// <summary> Amount of orders </summary>
        [JsonPropertyName("rows")]
        public int Rows { get; set; } = 1;

        /// <summary> Amount of orders </summary>
        [JsonPropertyName("tradeType")]
        public e_TradeType TradeType { get; set; }

        /// <summary> Crypto </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = "usdt";

        /// <summary> Fiat </summary>
        [JsonPropertyName("fiat")]
        public string Fiat { get; set; } = TelegramBotSettings.Fiat;

        /// <summary> Fiat amount </summary>
        [JsonPropertyName("transAmount")]
        public int? TransAmount { get; set; }

        /// <summary> Pay Types </summary>
        [JsonPropertyName("payTypes")]
        public List<string>? PayTypes { get; set; }
    }
}
