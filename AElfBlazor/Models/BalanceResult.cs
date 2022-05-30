using System;
using System.Text.Json.Serialization;

namespace AElfBlazor.Models
{
    public class BalanceResult
    {
        [JsonNumberHandling(JsonNumberHandling.AllowReadingFromString)] 
        public long Balance { get; set; } = 0;
        public string Owner { get; set; } = default!;
        public string Symbol { get; set; } = default!;

        public decimal ELFBalance => Balance / 100000000m;
    }
}
