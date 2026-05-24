using Models;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class ItemDB : BaseDB<Item>
    {
        protected override string GetTableName()
        {
            return "item";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Item> CreateModelAsync(object[] row)
        {
            Item i = new Item();
            i.Id = int.Parse(row[0].ToString());
            i.Name = row[1].ToString();
            i.Description = row[2].ToString();
            i.Rarity = float.Parse(row[3].ToString());
            i.Price = int.Parse(row[4].ToString());
            return i;
        }
        public async Task<List<Item>> GetAllAsync()
        {
            return ((List<Item>)await SelectAllAsync());
        }
        public async Task<List<Item>> GetByPlayerIdAsync(int playerId)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("playerid", playerId);
            return ((List<Item>)await SelectAllAsync(filterValues));

        }
        public async Task<Item> InsertAsync(Item i)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
             {
                 {"name",i.Name},{"description",i.Description},{"rarity",i.Rarity},{"price",i.Price}
             };
            return (Item)await base.InsertGetObjAsync(fillvalues);
        }
        public async Task<int> UpdateAsync(Item i)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();
            fillvalues.Add("name", i.Name);
            fillvalues.Add("description", i.Description);
            fillvalues.Add("rarity", i.Rarity);
            fillvalues.Add("price", i.Price);
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", i.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(Item i)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", i.Id);
            return await base.DeleteAsync(filterValues);
        }

        //page functions
        public async Task AquireItemAsync(Item i, int playerId)
        {
            InventoryDB inventoryDB = new InventoryDB();
            Inventory inventory = new Inventory();

            if (await inventoryDB.GetByPlayerAndItemIdAsync(playerId, i.Id) != null)
            {
                List<Inventory> temp = await inventoryDB.GetByPlayerAndItemIdAsync(playerId, i.Id);
                inventory = temp[0];
                inventory.Quantity += 1;
                await inventoryDB.UpdateAsync(inventory);
            }
            else
            {
                inventory.ItemId = i.Id;
                inventory.PlayerId = playerId;
                inventory.Quantity = 1;
                await inventoryDB.InsertAsync(inventory);
            }
        }
        public async Task<string> RemoveItemAsync(Item i, int playerId, int quantity)
        {
            InventoryDB inventoryDB = new InventoryDB();
            Inventory inventory = new Inventory();
            List<Inventory> temp = await inventoryDB.GetByPlayerAndItemIdAsync(playerId, i.Id);
            inventory = temp[0];
            if (inventory != null && inventory.Quantity >= quantity)
            {
                if (inventory.Quantity > quantity)
                {
                    inventory.Quantity -= quantity;
                }
                else if (inventory.Quantity == quantity)
                {
                    await inventoryDB.DeleteAsync(inventory);
                    return "Items removed";
                }
                await inventoryDB.UpdateAsync(inventory);
                return "Items removed ";
            }
            else
            {
                return $"You dont have enough {i.Name} to upgrade your character";
            }
        }

       
    }
}
