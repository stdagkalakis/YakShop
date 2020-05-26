using System.Text.RegularExpressions;
using System;
using Newtonsoft.Json;

namespace YakShop.API.Models
{
    [Serializable]
    public class OrderDetail
    {

        public Guid Id { get; set; }
        [JsonProperty(PropertyName = "customer")]
        public string Customer { get; set; }
        [JsonProperty(PropertyName = "order")]
        public Order Order { get; set; }

        public bool ShouldSerializeId()
        {
            return false;
        }
    }

    public class Order
    {
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "milk")]
        public double Milk
        {
            set; get;
        }

        [JsonProperty(PropertyName = "skins")]
        public int Skins { get; set; }
        public bool ShouldSerializeId()
        {
            return false;
        }

        public bool ShouldSerializeSkins()
        {
            return Skins > 0;
        }
        public bool ShouldSerializeMilk()
        {
            return Milk > 0;
        }
    }
}