using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
namespace DBL
{
    public class DevBanDB:BaseDB<DevBan>
    {
        //primary Functions
        protected override string GetTableName()
        {
            return "devban";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<DevBan> CreateModelAsync(object[] row)
        {
            DevBan db = new DevBan();
            db.Id = int.Parse(row[0].ToString());
            db.IsBanned = row[1].ToString() == "1" ? true : false;
            db.IsWarned = row[2].ToString() == "1" ? true : false;
            db.BanTime = DateTime.Parse(row[3].ToString());
            db.BanReason = row[4].ToString();
            db.UserID = int.Parse(row[5].ToString());
            return db;
        }
        public async Task<List<DevBan>> GetAllAsync()
        {
            return ((List<DevBan>)await SelectAllAsync());
        }
       public async Task<DevBan> InsertAsync(DevBan db)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"IsBanned",db.IsBanned?1:0},
                {"IsWarned",db.IsWarned?1:0},
                {"BanTime",db.BanTime},
                {"BanReason",db.BanReason},
                {"UserID",db.UserID}
            };
            return (DevBan)await base.InsertGetObjAsync(fillvalues);
        }
        public async Task<int> UpdateAsync(DevBan db,Consumer c)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"IsBanned",db.IsBanned?1:0},
                {"IsWarned",db.IsWarned?1:0},
                {"BanTime",db.BanTime},
                {"BanReason",db.BanReason},
                
            };
            Dictionary<string, object> filterValues = new Dictionary<string, object>()
            {
                {"userid",c.Id}
            };
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(DevBan db)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>()
            {
                {"id",db.Id}
            };
            return await base.DeleteAsync(filterValues);
        }
        public async Task<DevBan> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("userid", id);
            List<DevBan> db = (List<DevBan>)await base.SelectAllAsync(filterValues);
            if (db.Count == 1)
            {
                return db[0];
            }
            else
            {
                return null;
            }

        }


        //Page Functions
        public async Task<int> CreateDevBanAsync(Consumer c)
        {
            DevBan db = new DevBan();
            db.IsBanned = false;
            db.IsWarned = false;
            db.BanTime = DateTime.Now;
            db.BanReason = "";
            db.UserID = c.Id;
            return (await InsertAsync(db)) !=null? 1:0;
        }
        public async Task<object> BanUser(Consumer c, string reason, DateTime time)
        {
            DevBan db = new DevBan();
            db.IsBanned = true;
            db.IsWarned = false;
            db.BanTime = time;
            db.BanReason = reason;
            return await UpdateAsync(db, c);

        }
        public async Task<object> WarnUser(Consumer c, string reason)
        {
            DevBan db = new DevBan();
            db.IsBanned = false;
            db.IsWarned = true;
            db.BanTime = DateTime.Now;
            db.BanReason = reason;
            return await UpdateAsync(db, c);
        }
    }
}
