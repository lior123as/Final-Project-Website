using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rem { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        
        

        public Player()
        {

        }
        public Player(int rem, int userId, string name, string type)
        {
            Rem = rem;
            UserId = userId;
            Name = name;
            Type = type;
        }
        public Player(Player p)
        {
            Rem = p.Rem;
            UserId = p.UserId;
            Name = p.Name;
            Type = p.Type;

        }
    }
}
