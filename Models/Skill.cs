using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PointsRequired { get; set; }
    
        public Skill()
        {

        }
        public Skill(string name,string description,int pointsReq)
        {
           
            Name = name;
            Description = description;
            PointsRequired = pointsReq;
         
        }
        public Skill(Skill s)
        {

            Name = s.Name;
            Description = s.Description;
            PointsRequired = s.PointsRequired;
           
        }
    }
}
