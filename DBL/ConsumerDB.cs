using Models;
using Resend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace DBL
{
    public class ConsumerDB : BaseDB<Consumer>
    {
        //primary Functions
        


       

        protected override string GetTableName()
        {
            return "users";
        }
        protected override string GetPrimaryKeyName()
        {
            return "id";
        }
        protected override async Task<Consumer> CreateModelAsync(object[] row)
        {
            Consumer c = new Consumer();
            c.Id = int.Parse(row[0].ToString());
            c.Username = row[1].ToString();
            c.Email = row[3].ToString();
            if (row[4].ToString() == "0")
            {
                c.IsDev = false;
            }
            else
            {
                c.IsDev = true;
            }
            return c;
        }
        public async Task<List<Consumer>> GetAllAsync()
        {
            return ((List<Consumer>)await SelectAllAsync());
        }
        public async Task<Consumer> InsertAsync(Consumer c, string password)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>()
            {
                {"Username",c.Username},{"Password",password},{"Email",c.Email},{"IsDev",c.IsDev?1:0}

            };
            return (Consumer)await base.InsertGetObjAsync(fillvalues);

        }
        public async Task<int> UpdateAsync(Consumer c)
        {
            Dictionary<string, object> fillvalues = new Dictionary<string, object>();

            fillvalues.Add("Username", c.Username);




            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", c.Id);
            return await base.UpdateAsync(fillvalues, filterValues);
        }
        public async Task<int> DeleteAsync(Consumer c)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", c.Id);
            return await base.DeleteAsync(filterValues);
        }
        public async Task<Consumer> GetByIdAsync(int id)
        {
            Dictionary<string, object> filterValues = new Dictionary<string, object>();
            filterValues.Add("id", id);
            List<Consumer> consumers = (List<Consumer>)await base.SelectAllAsync(filterValues);
            if (consumers.Count == 1)
            {
                return consumers[0];
            }
            else
            {
                return null;
            }

        }


        //Page Functions
        public async Task<Dictionary<int, string>> RegisterAsync(Consumer consumer, string password)
        {
            Dictionary<int, string> result = new Dictionary<int, string>();
            result.Add(0, "");
            result.Add(1, "");
            result.Add(2, "");
            result.Add(3, "");
            result.Add(4, "");
            result.Add(5, "");
            result.Add(6, "");

            List<Consumer> consumers = (List<Consumer>)await GetAllAsync();
            bool hasProblems = false;
            if (consumers.Any(c => c.Username == consumer.Username))
            {
                result[0] = "Username already exists";
                hasProblems = true;
            }
            if (consumer.Username.Length > 100)
            {
                result[0] = "Username too long";
                hasProblems = true;
            }
            if (consumers.Any(c => c.Email == consumer.Email))
            {
                result[1] = "Email already in use";
                hasProblems = true;
            }
            else if (!await IsValidDomain(consumer.Email))
            {
                result[1] = "Email domain is not valid";
                hasProblems = true;
            }
            if (password.Length < 8)
            {
                result[2] = "Password must be at least 8 characters long";
                hasProblems = true;
            }
            if (password.Length > 20)
            {
                result[2] = "Password is too long";
                hasProblems = true;
            }
            for (int i = 0; i < 10; i++)
            {
                if (password.Contains(i.ToString()))
                {
                    break;
                }
                if (i == 9)
                {
                    result[3] = "Password must contain at least one number";
                    hasProblems = true;
                }
            }
            for (int i = 65; i <= 90; i++)
            {
                char c = (char)i;
                if (password.Contains(c))
                {
                    break;
                }
                if (i == 90)
                {
                    result[4] = "Password must contain at least one uppercase letter";
                    hasProblems = true;
                }
            }
            for (int i = 97; i <= 122; i++)
            {
                char c = (char)i;
                if (password.Contains(c))
                {
                    break;
                }
                if (i == 122)
                {
                    result[5] = "Password must contain at least one lowercase letter";
                    hasProblems = true;
                }
            }

            if (hasProblems)
            {
                return result;
            }
            else
            {

                Consumer c = await InsertAsync(consumer, password);
                DevBanDB DBDB = new DevBanDB();
                await DBDB.CreateDevBanAsync(c);
                result[6] = "You have been added to the system";
                return result;
            }




        }
        public async Task<(Consumer, string)> LoginAsync(string username, string password)
        {
            List<Consumer> consumers = (List<Consumer>)await SelectAllAsync(new Dictionary<string, object>()
            {
                {"Username",username },
                {"Password",password }
            });
            string ret = "";
            if (consumers.Count == 1)
            {
                DevBanDB DBDB = new DevBanDB();
                DevBan db = (DevBan)(await DBDB.GetByIdAsync(consumers[0].Id));
                if (db.IsBanned)
                {
                    if (db.BanTime < DateTime.Now)
                    {
                        db.IsBanned = false;
                        db.BanReason = "";

                        await DBDB.UpdateAsync(db, consumers[0]);
                        return (consumers[0], null);
                    }
                    else
                    {
                        ret += "You are banned until " + db.BanTime.ToString() + "\nfor the reason: " + db.BanReason;
                    }
                    return (null, ret);
                }

            }
            else
            {
                ret += "Incorrect username or password";
                return (null, ret);
            }
            return (consumers[0], null);


        }
        public async Task<string> DeleteUserAsync(Consumer c, string password)
        {
            List<Consumer> consumers = (List<Consumer>)await SelectAllAsync(new Dictionary<string, object>()
            {

                {"Password",password }
            });
            if (consumers.Count == 0)
            {
                return "Incorrect password";
            }
            else
            {
                await DeleteAsync(c);
                return "User deleted successfully";
            }
        }

        public async Task<bool> IsValidDomain(string email)
        {
            try
            {
                string domain = email.Split('@')[1];
                // Checks if the domain exists and can resolve to an IP address
                var hostEntry = await Dns.GetHostEntryAsync(domain);
                return hostEntry.AddressList.Length > 0;
            }
            catch
            {
                return false; // Domain does not exist or has no mail servers
            }
        }//checks if the email is valid
        
    }
}
