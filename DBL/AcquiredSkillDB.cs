using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class AcquiredSkillDB:BaseDB<AcquiredSkill>
    {
        protected override string GetTableName()
        {
            return "acquiredskill";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<AcquiredSkill> CreateModelAsync(object[] row)
        {
            AcquiredSkill askill = new AcquiredSkill();
            askill.Id = int.Parse(row[0].ToString());
            askill.AcquiredHeroesId = int.Parse(row[1].ToString());
            askill.SkillId = int.Parse(row[2].ToString());
            askill.IsAcquired = int.Parse(row[3].ToString());
            return askill;
        }
        public async Task<List<AcquiredSkill>> GetAllAsync()
        {
            return ((List<AcquiredSkill>)await SelectAllAsync());
        }
        public async Task<AcquiredSkill> InsertAsync(AcquiredSkill askill)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"acquiredheroesid",askill.AcquiredHeroesId},{"skillid",askill.SkillId},{"isaqcuired",askill.IsAcquired}
            };
            return (AcquiredSkill)await base.InsertGetObjAsync(fillvalues);
        }
         public async Task<int> UpdateAsync(AcquiredSkill askill)
         {
             Dictionary<string, object> fillvalues = new Dictionary<string, object>();
             fillvalues.Add("acquiredheroesid", askill.AcquiredHeroesId);
             fillvalues.Add("skillid", askill.SkillId);
             fillvalues.Add("isacquired", askill.IsAcquired);
             Dictionary<string, object> filterValues = new Dictionary<string, object>();
             filterValues.Add("id", askill.Id);
             return await base.UpdateAsync(fillvalues, filterValues);
        } 
            public async Task<int> DeleteAsync(AcquiredSkill askill)
            {
                Dictionary<string, object> filterValues = new Dictionary<string, object>();
                filterValues.Add("id", askill.Id);
                return await base.DeleteAsync(filterValues);
        }

    }
}
