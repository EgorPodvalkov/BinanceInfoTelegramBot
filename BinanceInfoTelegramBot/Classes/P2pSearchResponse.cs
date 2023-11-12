using Newtonsoft.Json;

namespace BinanceInfoTelegramBot.Classes
{
    public class P2pSearchResponse
    {

        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("messageDetail")]
        public string? MessageDetail { get; set; }

        [JsonProperty("data")]
        public List<Order>? Orders { get; set; }


    }

    public class Order
    {
        [JsonProperty("adv")]
        public OrderDetail OrderDetail { get; set; }

        [JsonProperty("advertiser")]
        public OrderCreator OrderCreator { get; set; }
    }

    public class OrderDetail
    {
        [JsonProperty("price")]
        public decimal Price { get; set; }

        [JsonProperty("asset")]
        public string Crypto { get; set; }

        [JsonProperty("fiatSymbol")]
        public string FiatSymbol { get; set; }

        [JsonProperty("maxSingleTransAmount")]
        public decimal MaxSingleTransAmount { get; set; }

        [JsonProperty("minSingleTransAmount")]
        public decimal MinSingleTransAmount { get; set; }

        [JsonProperty("payTimeLimit")]
        public int PayTimeLimit { get; set; }

        [JsonIgnore]
        public List<string> PayTypes
        {
            get => tradeMethods.Select(x => x.Identifier).ToList();
            set => tradeMethods = value.Select(x => new TradeMethod { Identifier = x }).ToList();
        }

        [JsonProperty("tradeMethods")]
        private List<TradeMethod> tradeMethods { get; set; }

        private class TradeMethod
        {
            [JsonProperty("identifier")]
            public string Identifier { get; set; }
        }
    }


    public class OrderCreator
    {
        [JsonProperty("nickName")]
        public string NickName { get; set; }

    }
}
