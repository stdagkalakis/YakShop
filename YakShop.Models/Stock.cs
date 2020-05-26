using System;
using Newtonsoft.Json;
using System.Collections.Generic;
namespace YakShop.Models
{
    public class Stock
    {
        [JsonProperty(PropertyName = "milk")]
        public double litersOfMik { get; set; }
        [JsonProperty(PropertyName = "skins")]
        public int skinsOfWool { get; set; }
        public Stock(List<Yak> herb, int days)
        {
            foreach (Yak yak in herb)
            {
                litersOfMik += yak.Milk(days);  // Gather Milk.
                skinsOfWool += yak.Shave(days); // Gather Wool.
                // yak.IncreaseAgeByDays(days);    // Increase age.
            }
            litersOfMik = Math.Round(litersOfMik, 2);
        }


    }
}