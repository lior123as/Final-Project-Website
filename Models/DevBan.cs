using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DevBan
    {
        public int Id { get; set; }
        public bool IsBanned { get; set; }
        public bool IsWarned { get; set; }
        public DateTime BanTime { get; set; }
        public string BanReason { get; set; }
        public int UserID { get; set; }
        public DevBan()
        {
        }
        public DevBan(int id, bool isBanned, bool isWarned, DateTime banTime,string banReason, int userID)
        {
            Id = id;
            IsBanned = isBanned;
            IsWarned = isWarned;
            BanTime = banTime;
            BanReason = banReason;
            UserID = userID;
        }
    }
}
