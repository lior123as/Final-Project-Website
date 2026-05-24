using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class PlayerDB:BaseDB<Player>
    {
        protected override string GetTableName()
        {
            return "player";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }

        protected override async Task<Player> CreateModelAsync(object[] row)
        {
            Player p = new Player();
            p.Id = int.Parse(row[0].ToString());
            p.Name = row[1].ToString();
            p.Rem = int.Parse(row[2].ToString());
            p.Type = row[3].ToString();
            p.UserId = int.Parse(row[4].ToString());

            return p;
        }
        public async Task<List<Player>> GetAllAsync()
        {
            return ((List<Player>)await SelectAllAsync());
        }
        public async Task<List<Player>> GetByUserIdAsync(int userId)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("users_id", userId);
            return ((List<Player>)await SelectAllAsync(filterValues));
        }
        public async Task<Player> InsertAsync(Player p)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"rem",p.Rem},{"users_id",p.UserId},{"name",p.Name},{"type",p.Type}

            };
            return (Player)await base.InsertGetObjAsync(fillvalues);

        }
        public async Task<int> UpdateAsync(Player p)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();

            
            fillvalues.Add("rem", p.Rem);
            fillvalues.Add("name", p.Name);
            fillvalues.Add("type", p.Type);

            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", p.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(Player p)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", p.Id);
            return await base.DeleteAsync(filterValues);
        }
        public async Task<Player> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            List<Player> players = (List<Player>)await base.SelectAllAsync(filterValues);
            if (players.Count == 1)
            {
                return players[0];
            }
            else
            {
                return null;
            }

        }

        //Page Functions
        public async Task<Player> CreatePlayerAsync(int userId,string name, string type)
        {
            Player p = new Player();
            p.Name = name;
            p.Rem = 0;
            p.UserId = userId;
            p.Type = type;  
         
            return await InsertAsync(p);
        }
        
        
    }
}
