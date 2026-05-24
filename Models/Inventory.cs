using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
         public Inventory()
        {

        }
            public Inventory(int playerId, int itemId, int quantity)
            {
                PlayerId = playerId;
                ItemId = itemId;
                this.Quantity = quantity;
            }
            public Inventory(Inventory i)
            {
                PlayerId = i.PlayerId;
                ItemId = i.ItemId;
                Quantity = i.Quantity;
        }
    }
}
