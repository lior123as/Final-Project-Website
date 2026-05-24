using Models;
using Mysqlx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBL
{
    public class SkillConDB:BaseDB<SkillCon>
    {
        protected override string GetTableName()
        {
            return "connections";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<SkillCon> CreateModelAsync(object[] row)
        {
            SkillCon sc = new SkillCon();
            sc.Id = int.Parse(row[0].ToString());
            sc.Skill = int.Parse(row[1].ToString());
            sc.SkillAfter = int.Parse(row[2].ToString());
            return sc;
        }
        public async Task<List<SkillCon>> GetAllAsync()
        {
            return ((List<SkillCon>)await SelectAllAsync());
        }
        public async Task<SkillCon> InsertAsync(SkillCon sc)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
               {"skill",sc.Skill},{"skill_after",sc.SkillAfter}

            };
            return (SkillCon)await base.InsertGetObjAsync(fillvalues);
        }
        public async Task<int> UpdateAsync(SkillCon sc)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();

            
            fillvalues.Add("skill_after", sc.SkillAfter);


            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", sc.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(SkillCon sc)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", sc.Id);
            return await base.DeleteAsync(filterValues);
        }
        public async Task<SkillCon> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            List<SkillCon> skillConnections = (List<SkillCon>)await base.SelectAllAsync(filterValues);
            if (skillConnections.Count == 1)
            {
                return skillConnections[0];
            }
            else
            {
                return null;
            }

        }

        //page functions
        public async Task<List<SkillCon>> GetConnectionsAsync(Skill s)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("skill", s.Id);
            filterValues.Add("skill_after", s.Id);
            List<SkillCon> skillConnections = (List<SkillCon>)await base.SelectAllAsync(filterValues);
            if (skillConnections.Count >= 1)
            {
                return skillConnections;
            }
            else
            {
                return null;
            }
            
        }//gets all of the connections for a skill
        public async Task<SkillCon> GetConnectionsSkillAfterAsync(Skill s)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("skill_after", s.Id);
            List<SkillCon> skillConnections = (List<SkillCon>)await base.SelectAllAsync(filterValues);
            if (skillConnections.Count == 1)
            {
                return skillConnections[0];
            }
            else
            {
                return null;
            }
        }//gets all the connections where the skill is the next one
        public async Task<List<SkillCon>> GetConnectionsSkillAsync(Skill s)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("skill", s.Id);
            List<SkillCon> skillConnections = (List<SkillCon>)await base.SelectAllAsync(filterValues);
            if (skillConnections.Count >= 1)
            {
                return skillConnections;
            }
            else
            {
                return null;
            }
        }//gets all the connections where the skill is connected to another one

        public async Task DeleteBySkillAsync(Skill s)
        {
            SkillCon scAfter = await GetConnectionsSkillAfterAsync(s);
            int beforeID = scAfter.Skill;
            foreach (SkillCon sc in await GetConnectionsSkillAsync(s) ?? new List<SkillCon>())
            {
                SkillCon newsc = new SkillCon();
                newsc.Skill = beforeID;
                newsc.SkillAfter = sc.SkillAfter;
                await InsertAsync(newsc);
                await DeleteAsync(sc);
            }
            await DeleteAsync(scAfter);

            
        }//deletes all of the connections to the skill and creates new connections to skills before, mostly for the deleteAsync for Skill
        public async Task CreateConnectionsForNewSkillAsync(Skill s,int idSB)
        {
            SkillCon sc = new SkillCon();
            sc.Skill = idSB;
            sc.SkillAfter = s.Id;
            await InsertAsync(sc);
        }//creates the connection for the new skill created
        public async Task CreateConnectionsForNewSkillAsync(Skill s, int idSB, List<int> skillsIDAfter)
        {
            List<SkillCon> listSC = new List<SkillCon>();
            SkillCon sc = new SkillCon();
            SkillConDB scDB = new SkillConDB();
            Skill st = new Skill();
            sc.Skill = idSB;
            sc.SkillAfter = s.Id;
            listSC.Add(sc);
            foreach (int idSA in skillsIDAfter)
            {
                st.Id = idSA;
                SkillCon sc2 = new SkillCon();
                sc2.Skill = s.Id;
                sc2.SkillAfter = idSA;
                listSC.Add(sc2);
                sc2 = await scDB.GetConnectionsSkillAfterAsync(st);
                await scDB.DeleteAsync(sc2);
            }
            foreach (SkillCon scInsert in listSC)
            {
                await InsertAsync(scInsert);
               
            }
        }//creates the connections for a skill thats inbetween two or more skills
    }
}
