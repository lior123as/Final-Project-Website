using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Enemies
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int InitialHP { get; set; }
        public string Attacks { get; set; }
        public string InitialAttackDamageRange { get; set; }
        public string AttackType { get; set; }

        public Enemies()
        {
        }
        public Enemies(int id, string name, string description, int initialHP, string attacks, string initialAttackDamageRange, string attackType)
        {
            Id = id;
            Name = name;
            Description = description;
            InitialHP = initialHP;
            Attacks = attacks;
            InitialAttackDamageRange = initialAttackDamageRange;
            AttackType = attackType;
        }
    }
}
