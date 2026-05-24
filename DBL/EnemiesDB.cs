using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class EnemiesDB : BaseDB<Enemies>
    {
        protected override string GetTableName()
        {
            return "enemies";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Enemies> CreateModelAsync(object[] row)
        {
            Enemies e = new Enemies();
            e.Id = int.Parse(row[0].ToString());
            e.Name = row[1].ToString();
            e.Description = row[2].ToString();
            e.InitialHP = int.Parse(row[3].ToString());
            e.Attacks = row[4].ToString();
            e.InitialAttackDamageRange = row[5].ToString();
            e.AttackType = row[6].ToString();
            return e;
        }
        public async Task<List<Enemies>> GetAllAsync()
        {
            return ((List<Enemies>)await SelectAllAsync());
        }
        public async Task<List<Enemies>> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            return (List<Enemies>)await base.SelectAllAsync(filterValues);
        }
        public async Task<Enemies> InsertAsync(Enemies e)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
                {
                    {"name",e.Name},{"description",e.Description},{"initialhp",e.InitialHP},{"attacks",e.Attacks},{"initialattackdamagerange",e.InitialAttackDamageRange},{"attacktype",e.AttackType}
                };
            return (Enemies)await base.InsertGetObjAsync(fillvalues);
        }
        public async Task<int> UpdateAsync(Enemies e)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();
            fillvalues.Add("name", e.Name);
            fillvalues.Add("description", e.Description);
            fillvalues.Add("initialhp", e.InitialHP);
            fillvalues.Add("attacks", e.Attacks);
            fillvalues.Add("initialattackdamagerange", e.InitialAttackDamageRange);
            fillvalues.Add("attacktype", e.AttackType);
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", e.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            return await base.DeleteAsync(filterValues);

        }
    }
}
