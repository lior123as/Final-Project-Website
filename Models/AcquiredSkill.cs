using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcquiredSkill
    {
        public int Id { get; set; }
        public int AcquiredHeroesId { get; set; }
        public int SkillId { get; set; }
        public int IsAcquired { get; set; }
        public AcquiredSkill()
        {
        }
        public AcquiredSkill(int aquiredHeroesId, int skillId, int isAquired)
        {
            AcquiredHeroesId = aquiredHeroesId;
            SkillId = skillId;
            IsAcquired = isAquired;
        }
    }
}
