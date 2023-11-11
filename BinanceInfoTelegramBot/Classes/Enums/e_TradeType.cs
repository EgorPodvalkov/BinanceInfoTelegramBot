using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BinanceInfoTelegramBot.Classes
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum e_TradeType
    {
        BUY,
        SELL,
    }
}
