using System;

namespace AElfBlazor.Models
{
    public class BalanceResult
    {
        public string Balance { get; set; } = "0";
        public string Owner { get; set; } = default!;
        public string Symbol { get; set; } = default!;

        public decimal ELFBalance => Convert.ToInt64(Balance) / 100000000m;
    }
}
