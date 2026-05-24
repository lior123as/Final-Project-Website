using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Rarity { get; set; }
        public int Price { get; set; }

       public Item()
        {
        }
        public Item(string name, string description, float rarity, int price)
        {
            Name = name;
            Description = description;
            Rarity = rarity;
            Price = price;
        }
        public Item(Item i)
        {
            Name = i.Name;
            Description = i.Description;
            Rarity = i.Rarity;
            Price = i.Price;
        }
    }
}
