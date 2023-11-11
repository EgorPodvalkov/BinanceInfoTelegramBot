namespace BinanceInfoTelegramBot.Classes
{
    /// <summary> Class for parsing parametrs from command text </summary>
    public class BuySellParametrs
    {
        public readonly string Asset = "usdt";
        public readonly int? TransAmount;
        public readonly List<string>? PayTypes;

        /// <summary> Returns object with parsed parametrs from command text </summary>
        public BuySellParametrs(string text)
        {
            var parametrs = text.Split(' ').ToList();

            // !p2pbuy 
            if (parametrs.Count == 1)
                return;

            // !p2pbuy btc
            if (parametrs.Count == 2)
            {
                Asset = parametrs[1];
                return;
            }

            // skiped handled params
            var unhandledParams = parametrs.Skip(2);

            // trans amount handling
            if (int.TryParse(unhandledParams.FirstOrDefault(), out int transAmount))
            {
                TransAmount = transAmount;
                unhandledParams = parametrs.Skip(1);
            }

            if (!unhandledParams.Any())
                return;

            // Pay types handling
            PayTypes = new List<string>();
            foreach (var p in unhandledParams)
            {
                PayTypes.Add(p);
            }
        }
    }
}
