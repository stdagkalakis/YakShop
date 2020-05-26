using Newtonsoft.Json;
namespace YakShop.API.Models
{
    public class YakModel
    {

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "age")]
        public double Age { get; set; }
        [JsonProperty(PropertyName = "age-last-shaved")]
        public double LastShavedAge { get; set; }
    }

}