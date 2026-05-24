using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AcquiredHeroes
    {
        public int Id {  get; set; }
        public int PlayerId { get; set; }
        public int HeroesId { get; set; }
        public int Acquired {  get; set; }
        public int Level { get; set; }
        public int Exp { get; set; }
        public int SkillPoints { get; set; }
        public AcquiredHeroes()
        {
        }
        public AcquiredHeroes(int playerId, int heroesId, int aquired, int level, int exp, int skillPoints)
        {
            PlayerId = playerId;
            HeroesId = heroesId;
            Acquired = aquired;
            Level = level;
            Exp = exp;
            SkillPoints = skillPoints;
        }


    }
}
