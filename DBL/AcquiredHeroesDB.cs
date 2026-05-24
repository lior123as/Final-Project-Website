using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class AcquiredHeroesDB:BaseDB<AcquiredHeroes>
    {
        protected override string GetTableName()
        {
            return "acquiredheroes";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<AcquiredHeroes> CreateModelAsync(object[] row)
        {
            AcquiredHeroes ah = new AcquiredHeroes();
            ah.Id = int.Parse(row[0].ToString());
            ah.PlayerId = int.Parse(row[1].ToString());
            ah.HeroesId = int.Parse(row[2].ToString());
            ah.Acquired = int.Parse(row[3].ToString());
            ah.Level = int.Parse(row[4].ToString());
            ah.Exp = int.Parse(row[5].ToString());
            ah.SkillPoints = int.Parse(row[6].ToString());
            return ah;
        }
        public async Task<List<AcquiredHeroes>> GetAllAsync()
        {
            return ((List<AcquiredHeroes>)await SelectAllAsync());
        }
        public async Task<List<AcquiredHeroes>> GetAllAsync(int playerId)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("playerid", playerId);
            return ((List<AcquiredHeroes>)await base.SelectAllAsync(filterValues));
        }
        public async Task<AcquiredHeroes> InsertAsync(AcquiredHeroes ah)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"playerid",ah.PlayerId},{"heroesid",ah.HeroesId},{"acquired",ah.Acquired},
                {"level",ah.Level},{"exp",ah.Exp},{"skillpoints",ah.SkillPoints}
            };
            return (AcquiredHeroes)await base.InsertGetObjAsync(fillvalues);
        }
        public async Task<int> UpdateAsync(AcquiredHeroes ah)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();
            
            fillvalues.Add("playerid", ah.PlayerId);
            fillvalues.Add("heroesid", ah.HeroesId);
            fillvalues.Add("acquired", ah.Acquired);
            fillvalues.Add("level", ah.Level);
            fillvalues.Add("exp", ah.Exp);
            fillvalues.Add("skillpoints", ah.SkillPoints);
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", ah.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(AcquiredHeroes ah)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", ah.Id);
            return await base.DeleteAsync(filterValues);
        }


    }
}
