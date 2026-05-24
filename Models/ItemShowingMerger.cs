using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ItemShowingMerger
    {
        public string name { get; set; }
        public string description { get; set; }
        public float rarity { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }

        public ItemShowingMerger(string name, string description, float rarity, int price, int quantity)
        {
            this.name = name;
            this.description = description;
            this.rarity = rarity;
            this.price = price;
            this.quantity = quantity;
        }
        public ItemShowingMerger()
        {
        }

    }
}
