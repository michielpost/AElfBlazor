using System.Text.Json.Serialization;

namespace AelfFund.Web.Models
{
    public class TransactionStatusResult
    {
        [JsonPropertyName("TransactionId")]
        public string TransactionId { get; set; } = default!;

        [JsonPropertyName("Status")]
        public string Status { get; set; } = default!;

        //[JsonPropertyName("Logs")]
        //public List<object> Logs { get; set; }

        [JsonPropertyName("Bloom")]
        public string Bloom { get; set; } = default!;

        [JsonPropertyName("BlockNumber")]
        public long BlockNumber { get; set; }

        [JsonPropertyName("BlockHash")]
        public string BlockHash { get; set; } = default!;

        [JsonPropertyName("Transaction")]
        public Transaction? Transaction { get; set; }

        [JsonPropertyName("ReturnValue")]
        public string? ReturnValue { get; set; }

        //[JsonPropertyName("Error")]
        //public object Error { get; set; }

        [JsonPropertyName("TransactionSize")]
        public int TransactionSize { get; set; }
    }

    public class Transaction
    {
        [JsonPropertyName("From")]
        public string From { get; set; } = default!;

        [JsonPropertyName("To")]
        public string To { get; set; } = default!;

        [JsonPropertyName("RefBlockNumber")]
        public long RefBlockNumber { get; set; }

        [JsonPropertyName("RefBlockPrefix")]
        public string RefBlockPrefix { get; set; } = default!;

        [JsonPropertyName("MethodName")]
        public string? MethodName { get; set; }

        [JsonPropertyName("Params")]
        public string? Params { get; set; }

        [JsonPropertyName("Signature")]
        public string Signature { get; set; } = default!;
    }
}
