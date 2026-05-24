using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class SkillCon
    {
        public int Id { get; set; }
        public int Skill { get; set; }
 
        public int SkillAfter { get; set; }
        public SkillCon()
        {

        }
        public SkillCon(int id,int skill,int skillAfter)
        {
            Id = id;
            Skill = skill;
            
            SkillAfter = skillAfter;
        }
    }
}
