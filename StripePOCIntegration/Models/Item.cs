using Newtonsoft.Json;

namespace StripePOCIntegration.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
