using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Consumer
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsDev { get; set; }
        public Consumer()
        {

        }
        public Consumer(int id ,string username,string email, bool isdev = false) 
        {
            Id = id;
            Username = username;
            Email = email;
            IsDev = isdev;
        }
        public Consumer(Consumer c)
        {
            Id = c.Id;  
            Username = c.Username;
            Email = c.Email;
            IsDev = c.IsDev;

        }
    }
}
