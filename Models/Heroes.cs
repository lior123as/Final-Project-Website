using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Heroes
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int StarterHp { get; set; }
        public string Type { get; set; }
        public string AttackType { get; set; }
        public string BasicAttack { get; set; }
        public string SpecialAttack { get; set; }
        public string UltimateAttack { get; set; }
       

        public Heroes()
        {
        }
        public Heroes(string name, string description, int starterHp, string type, string attackType, string basicAttack, string specialAttack, string ultimateAttack)
        {
            Name = name;
            Description = description;
            StarterHp = starterHp;
            Type = type;
            AttackType = attackType;
            BasicAttack = basicAttack;
            SpecialAttack = specialAttack;
            UltimateAttack = ultimateAttack;
            
        }
    }
}
