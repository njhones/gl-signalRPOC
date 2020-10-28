
using Newtonsoft.Json;

namespace NegociationServer
{
    public class Message
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        
        [JsonProperty("sender")]
        public string Sender { get; set; }
    }
}
