using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class SkillDB:BaseDB<Skill>
    {
        protected override string GetTableName()
        {
            return "skill";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Skill> CreateModelAsync(object[] row)
        {
            Skill s = new Skill();
            s.Id = int.Parse(row[0].ToString());
            s.Name = row[1].ToString();
            s.Description = row[2].ToString();
            s.PointsRequired = int.Parse(row[3].ToString());

            return s;
        }
        public async Task<List<Skill>> GetAllAsync()
        {
            return ((List<Skill>)await SelectAllAsync());
        }
        public async Task<Skill> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            List<Skill> skills = (List<Skill>)await base.SelectAllAsync(filterValues);
            if (skills.Count == 1)
            {
                return skills[0];
            }
            else
            {
                return null;
            }

        }
        public async Task<Skill> InsertAsync(Skill s)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"name",s.Name},{"description",s.Description},{"pointsRequired",s.PointsRequired}

            };
            return (Skill)await base.InsertGetObjAsync(fillvalues);

        }
        public async Task<int> UpdateAsync(Skill s)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();

            fillvalues.Add("name", s.Name);
            fillvalues.Add("description", s.Description);
            fillvalues.Add("pointsRequired", s.PointsRequired);
  

            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", s.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }//updates a skill
        public async Task<int> DeleteAsync(Skill s)
        {
            SkillConDB scDB = new SkillConDB();
            await scDB.DeleteBySkillAsync(s);
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", s.Id);
            return await base.DeleteAsync(filterValues);
        }//deletes the skill and the connections for it by id

        //Page Functions
        public async Task<bool> CreateSkillAsync(Skill newSkill,int idSB)
        {
            
            Skill insertedSkill = await InsertAsync(newSkill);
            SkillConDB scDB = new SkillConDB();
            await scDB.CreateConnectionsForNewSkillAsync(insertedSkill, idSB);
            return insertedSkill != null && insertedSkill.Id > 0;
        }//creates the skill and the connections for it
        public async Task<bool> CreateSkillBetweenAsync(Skill newSkill, int idSB,List<int> idSA  )
        {
            Skill insertedSkill = await InsertAsync(newSkill);
            SkillConDB scDB = new SkillConDB();
            await scDB.CreateConnectionsForNewSkillAsync(insertedSkill, idSB, idSA);
            return insertedSkill != null && insertedSkill.Id > 0;

        }//creates the skill and the connections for it if its inbetween two or more skills
    }
}
