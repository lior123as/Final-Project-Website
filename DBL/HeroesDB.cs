using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class HeroesDB:BaseDB<Heroes>
    {
        protected override string GetTableName()
        {
            return "heroes";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Heroes> CreateModelAsync(object[] row)
        {
            Heroes h = new Heroes();
            h.Id = int.Parse(row[0].ToString());
            h.Name = row[1].ToString();
            h.Description = row[2].ToString();
            h.StarterHp = int.Parse(row[3].ToString());
            h.Type = row[4].ToString();
            h.AttackType = row[5].ToString();
            h.BasicAttack = row[6].ToString();
            h.SpecialAttack = row[7].ToString();
            h.UltimateAttack = row[8].ToString();
            
            return h;
        }
        public async Task<List<Heroes>> GetAllAsync()
        {
            return ((List<Heroes>)await SelectAllAsync());
        }
        public async Task<List<Heroes>> GetByPlayerIdAsync(int id)
        {
            AcquiredHeroesDB ahdb = new AcquiredHeroesDB();
            List<AcquiredHeroes> ah = await ahdb.GetAllAsync(id);
            int heroid = 0;
            List<Heroes> heroes = new List<Heroes>();  
            foreach (AcquiredHeroes h in ah)
            {
                heroid = h.HeroesId;
                heroes.AddRange(await GetByIdAsync(heroid));
            }
            return heroes;

        }
        public async Task<List<Heroes>> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            return (List<Heroes>)await base.SelectAllAsync(filterValues);
        }
        public async Task<Heroes> InsertAsync(Heroes h)
            {
                Dictionary<string, object> fillvalues = new Dictionary<string, object>()
                {
                    {"name",h.Name},{"description",h.Description},{"starterhp",h.StarterHp},{"type",h.Type},
                    {"attacktype",h.AttackType},{"basicattack",h.BasicAttack},{"specialattack",h.SpecialAttack},
                    {"ultimateattack",h.UltimateAttack}
                };
                return (Heroes)await base.InsertGetObjAsync(fillvalues);
    
            }
            public async Task<int> UpdateAsync(Heroes h)
            {
                Dictionary<string, object> fillvalues = new Dictionary<string, object>();
    
                
                fillvalues.Add("name", h.Name);
                fillvalues.Add("description", h.Description);
                fillvalues.Add("starterhp", h.StarterHp);
                fillvalues.Add("type", h.Type);
                fillvalues.Add("attacktype", h.AttackType);
                fillvalues.Add("basicattack", h.BasicAttack);
                fillvalues.Add("specialattack", h.SpecialAttack);
                fillvalues.Add("ultimateattack", h.UltimateAttack);
            
    
                Dictionary<string, object> filterValues = new Dictionary<string, object>();
                filterValues.Add("id", h.Id);
                return await base.UpdateAsync(fillvalues, filterValues);
            }
            public async Task<int> DeleteAsync(Heroes h)
            {
                Dictionary<string, object> filterValues = new Dictionary<string, object>();
                filterValues.Add("id", h.Id);
                return await base.DeleteAsync(filterValues);
            }   


    }
}
