using Newtonsoft.Json;

namespace StripePOCIntegration.Models
{
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
