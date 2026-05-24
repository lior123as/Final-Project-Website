using Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class InventoryDB:BaseDB<Inventory>
    {
        protected override string GetTableName()
        {
            return "inventory";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Inventory> CreateModelAsync(object[] row)
        {
            Inventory i = new Inventory();
            i.Id = int.Parse(row[0].ToString());
            i.PlayerId = int.Parse(row[1].ToString());
            i.ItemId = int.Parse(row[2].ToString());
            i.Quantity = int.Parse(row[3].ToString());
            return i;
        }
         public async Task<List<Inventory>> GetAllAsync()
        {
            return ((List<Inventory>)await SelectAllAsync());
        }
        public async Task<List<Inventory>> GetByPlayerIdAsync(int playerId)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("playerid", playerId);
            return ((List<Inventory>)await SelectAllAsync(filterValues));

        }
        public async Task<List<Inventory>> GetByPlayerAndItemIdAsync(int playerId,int itemId)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("playerid", playerId);
            filterValues.Add("itemid", itemId);
            return ((List<Inventory>)await SelectAllAsync(filterValues));

        }
        public async Task<List<ItemShowingMerger>> GetByQueryAsync(string query)
        {
            using MySqlConnection conn = new MySqlConnection(cmd.Connection.ConnectionString);
            conn.Open();
            using MySqlCommand cmdd = new MySqlCommand(query, conn);
            List<ItemShowingMerger> list = new List<ItemShowingMerger>();
            using MySqlDataReader reader = cmdd.ExecuteReader();

                while (reader.Read())
                {
                    ItemShowingMerger i = new ItemShowingMerger();
                    i.name = reader.GetString(0);
                    i.description = reader.GetString(1);
                    i.rarity = reader.GetFloat(2);
                    i.price = reader.GetInt32(3);
                    i.quantity = reader.GetInt32(4);
                    list.Add(i);
                }
                reader.Close();
            return list;
            
        }

        public async Task<Inventory> InsertAsync(Inventory i)
        {
             Dictionary<string, object> fillvalues = new Dictionary<string, object>()
             {
                 {"playerid",i.PlayerId},{"itemid",i.ItemId},{"quantity",i.Quantity}
             };
             return (Inventory)await base.InsertGetObjAsync(fillvalues);
         }
          public async Task<int> UpdateAsync(Inventory i)
        {
             Dictionary<string, object> fillvalues = new Dictionary<string, object>();
             fillvalues.Add("playerid", i.PlayerId);
             fillvalues.Add("itemid", i.ItemId);
             fillvalues.Add("quantity", i.Quantity);
             Dictionary<string, object> filterValues = new Dictionary<string, object>();
             filterValues.Add("id", i.Id);
             return await base.UpdateAsync(fillvalues, filterValues);
         }
          public async Task<int> DeleteAsync(Inventory i)
        {
             Dictionary<string, object> filterValues = new Dictionary<string, object>();
             filterValues.Add("id", i.Id);
             return await base.DeleteAsync(filterValues);
        }
        public async Task<List<ItemShowingMerger>> GetInventoryBySortingAsync(string sortBy, int playerId)
        {
            InventoryDB inventoryDB = new InventoryDB();
            string query = $"SELECT item.name, item.description, item.rarity, item.price, inventory.quantity FROM users INNER JOIN player ON player.users_id = users.id INNER JOIN inventory ON inventory.playerid = player.id INNER JOIN item ON inventory.itemid = item.id WHERE player.id = {playerId};"; ;
            if(sortBy == "quantity")
            {
                query = $"SELECT item.name, item.description, item.rarity, item.price, inventory.quantity FROM users INNER JOIN player ON player.users_id = users.id INNER JOIN inventory ON inventory.playerid = player.id INNER JOIN item ON inventory.itemid = item.id WHERE player.id = {playerId} ORDER BY inventory.quantity DESC;";
                return await inventoryDB.GetByQueryAsync(query);
            }
            else if (sortBy == "rarity")
            {
                query = $"SELECT item.name, item.description, item.rarity, item.price, inventory.quantity FROM users INNER JOIN player ON player.users_id = users.id INNER JOIN inventory ON inventory.playerid = player.id INNER JOIN item ON inventory.itemid = item.id WHERE player.id = {playerId} ORDER BY item.rarity Desc;";
                return await inventoryDB.GetByQueryAsync(query);
            }
            else
            {
                return await inventoryDB.GetByQueryAsync(query);
            }


        }
    }
}
