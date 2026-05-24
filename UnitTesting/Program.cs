using DBL;
using Models;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace UnitTesting
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //await ConsumerCheck();
            //await DevBanCheck();
            //var a = (1, "aa");
            //Console.WriteLine(a);
            //await SkillAndSkillConDBCheck();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5119/api/Game/");
            await ShowAllSkillsAPIAsync(client);
            await ShowUserByIDAPIAsync(client, 1);
            await LoginAPIAsync(client, "Lior", "1524XZS12!l");
        }
        //API Testing
        public static async Task ShowAllSkillsAPIAsync(HttpClient client)
        {
            try
            {
                Console.WriteLine("Trying to get all skills...");
                var response = await client.PostAsJsonAsync<List<Skill>>("GetAllSkills",null);
               response.EnsureSuccessStatusCode();
                Console.WriteLine("Success in getting data from API...");
                Console.WriteLine("Showing all skills...");
                var skills = await response.Content.ReadFromJsonAsync<List<Skill>>();
                foreach(var skill in skills)
                {
                    Console.WriteLine($"{skill.Id}\n{skill.Name}\n{skill.Description}\n{skill.PointsRequired}\n");
                }

            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static async Task ShowUserByIDAPIAsync(HttpClient client,int id)
        {
            try
            {
                Console.WriteLine("Trying to get user by id...");
                var response = await client.PostAsJsonAsync<int>("GetUserById", id);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Success in getting data from API...");
                Console.WriteLine("Showing user...");
                var user = await response.Content.ReadFromJsonAsync<Consumer>();
                if(user == null)
                {
                    Console.WriteLine("User not found");
                    
                }
                else
                {
                 
                    Console.WriteLine($"Username: {user.Username} Email: {user.Email} IsDev: {user.IsDev}");
                }    
                    
                

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public record logindata(string username, string password);
        public record returninfo(Consumer user, string response);
        public static async Task LoginAPIAsync(HttpClient client, string username,string password)
        {

            try
            {
                logindata loginData = new  logindata(username, password);
                Console.WriteLine("Trying to login...");
                var response = await client.PostAsJsonAsync<logindata>("Login", loginData);
                response.EnsureSuccessStatusCode();
                Console.WriteLine("Success in getting data from API...");
                Console.WriteLine("Showing user...");
                returninfo info = await response.Content.ReadFromJsonAsync<returninfo>();
                if (info.user == null)
                {
                    Console.WriteLine($"{info.response}");

                }
                else
                {

                    Console.WriteLine($"Username: {info.user.Username} Email: {info.user.Email} IsDev: {info.user.IsDev}");
                }



            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        //ConsumerDB Testing
        public static async Task ConsumerCheck()
        {


            ConsumerDB CBD = new ConsumerDB();
            List<Consumer> lst = new List<Consumer>();
            //lst = CBD.GetAllAsync().Result;
            //RunListConsumer(lst);
            //Consumer a = new Consumer(8, "g", "g");
            //await CBD.InsertAsync(a,"itsnay");
            //Consumer b = new Consumer(7, "hello22", "ni...@gmail.com");
            Consumer c = new Consumer(14, "Lior", "Shlior701@gmail.com");
            c.Username = "LiorUpdated";
            //Console.WriteLine(await CBD.UpdateAsync(b));
            //Console.WriteLine(await  CBD.DeleteAsync(b));
            //RunConsumer(await CBD.GetByIdAsync(4));
            Consumer t = CBD.LoginAsync(c.Username, "1524XZS12!l").Result.Item1;
            RunConsumer(t);
            Console.WriteLine(CBD.UpdateAsync(c).Result);
            //check(CBD.LoginAsync("Lior","Sup123bro").Result);
            //Console.WriteLine(await CBD.DeleteUserAsync(a,"g"));
        }
        public static void check(object c)
        {
            if (c is string)
            {
                Console.WriteLine(c);
            }
            else if (c is Consumer)
            {
                RunConsumer((Consumer)c);
            }

        }
        public static void RunConsumer(Consumer c)
        {
            if (c == null)
            {
                Console.WriteLine("Consumer not found");
                return;
            }
            else
            {
                Console.WriteLine($"{c.Id}\n{c.Username}\n{c.Email}\n{c.IsDev}\n");
            }
        }
        public static void RunListConsumer(List<Consumer> lst)
        {
            foreach (var item in lst)
            {
                Console.WriteLine($"{item.Id}\n{item.Username}\n{item.Email}\n{item.IsDev}\n");
            }
        }
        //DevBanDB Testing
        public static async Task DevBanCheck()
        {
            DevBanDB DBDB = new DevBanDB();
            ConsumerDB CDB = new ConsumerDB();
            Consumer c = new Consumer(1, "hi", "eee");

            //await DBDB.CreateDevBanAsync(c);
            //await DBDB.WarnUser(c,"Do not write Ni***r in chat");
            DateTime date = DateTime.Now.AddSeconds(40);
            await DBDB.BanUser(c, "Do not write Ni***r in chat, this is a repeating offence", date);
            while (DBDB.GetByIdAsync(c.Id).Result.BanTime > DateTime.Now)
            {
                Console.WriteLine(await CDB.LoginAsync("hi", "111"));
                Console.WriteLine(DBDB.GetByIdAsync(1).Result.BanTime - DateTime.Now);
                await Task.Delay(1000);
                Console.Clear();
            }

            Console.WriteLine(await CDB.LoginAsync("hi", "111"));
        }
        public static void RunDevBan(DevBan db)
        {
            if (db == null)
            {
                Console.WriteLine("DevBan not found");
                return;
            }
            else
            {
                Console.WriteLine($"{db.Id}\n{db.IsBanned}\n{db.IsWarned}\n{db.BanTime}\n{db.BanReason}\n{db.UserID}\n");
            }
        }
        //SkillDB and SkillConDB Testing
        public static async Task SkillAndSkillConDBCheck()
        {
            SkillDB SDB = new SkillDB();
            SkillConDB SCDB = new SkillConDB();
            Skill s = new Skill("Fireball", "Shoots a ball of fire", 5);
            Skill b = new Skill("Explode in a terror attack", "Fight like the muslims! Join the Jihad!", 4);
            Skill haha = new Skill("Haha", "Funny skill", 1);
            Skill hehe = new Skill("Hehe", "Funnier skill", 2);
            Skill hehehe = new Skill("Hehehe", "Funniest skill", 3);
            Skill copypasta = new Skill("Copypasta", "Copy paste skill", 1);
            Skill unoriginal = new Skill("Unoriginal", "Unoriginal skill", 2);
            Skill creepypasta = new Skill("Creepypasta", "Scary skill peenar toucher skill: summons the peenar snipper man to snip the peenars of your enemies. beCAUSE you summoned the peenar snipper man and not the enemy, the peenar snipper man will only take your foreskin. ALReady circumsized? Too bad, he will take your balls and play basketball while you roll around in unimaginable pain beCAUSE he doesn't use anesthesia!!1!!", 3);
            Skill wholesomepasta = new Skill("Wholesomepasta", "A skill that makes everyone feel good inside. Summons a warm feeling of love and acceptance, making all enemies too busy hugging each other to fight back.", 4);
            Skill truthnuke_soyjak_truke = new Skill("Truke", "A skill that unleashes the ultimate truth bomb, causing all enemies to question their existence and surrender immediately.", 5);
            //Skill the_biggest_piece_of_coal = new Skill(0, "The Biggest Piece of Coal", "A skill that transforms into a massive piece of coal, blocking all enemy attacks and providing an impenetrable defense for the user.", 6, 6);
            //await SDB.InsertAsync(s);
           // await SDB.CreateSkillAsync(copypasta,1);
            //await SDB.CreateSkillAsync(unoriginal, 1);
            
            RunSkillsAndConnections(await SDB.GetAllAsync(),await SCDB.GetAllAsync());



        }
        public static void RunSkill(Skill s)
        {
            if (s == null)
            {
                Console.WriteLine("Skill not found");
                return;
            }
            else
            {
                Console.WriteLine($"{s.Id}\n{s.Name}\n{s.Description}\n{s.PointsRequired}\n");
            }
        }
        public static void RunSkillsAndConnections(List<Skill> skills,List<SkillCon> skillConnections)
        {
            string s = "";
            int locX = 0;
            int locXadd = 0;
            int locY = 0;
            
            PrintMap(CreateSkill(skills[0]));
            foreach (var skill in skills)
            {
                
                foreach (var con in skillConnections)
                {
                    if (con.Skill == skill.Id)
                    {
                       
                        foreach(var nextSkill in skills)
                        {
                            if(nextSkill.Id== con.SkillAfter)
                            {
                                s = $"-{con.Skill}---{con.SkillAfter}-";
                                Console.Write(s);
                                PrintMap(CreateSkill(nextSkill));
                                locY++;
                                locX += nextSkill.Name.Length + s.Length+locXadd;
                                Console.SetCursorPosition(locX,locY);
                            }
                        }
                        locY = 0;
                        locXadd = skill.Name.Length+2;
                        

                    }
                    
                   
                }
            }

           
            
        }
        public static void PrintMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                
                    Console.Write(map[i, 1]);
                
                
            }
        }
        //public static char[,] CreateConnection(Skill s1, Skill s2, SkillCon sc,int loc)
        //{
        //    char[,] map = null;
        //    switch (loc)
        //    {
        //        case 0:
        //            map = new char[3, s1.Name.Length + s2.Name.Length + 5];
        //            break;
        //        case 1:
        //            map = new char[s1.Name.Length + s2.Name.Length + 5, 3];
        //            break;
        //        case 2:
        //            map = new char[3, s1.Name.Length + s2.Name.Length + 5];
        //            break;
        //        case 3:
        //            map = new char[s1.Name.Length + s2.Name.Length + 5, 3];
        //            break;
        //        default:
        //            break;
        //    }

        //    return map;
        //}
        public static char[,] CreateSkill(Skill s)
        {
            int Length = s.Name.Length+1; 
            char[,] map = new char[Length+2,3 ];
            for(int i = 0; i < map.GetLength(0); i++)
            {
                for(int j = 0; j < map.GetLength(1); j++)
                {
                    map[i, j] = ' ';
                }
               
            }
            
            for (int j = 0; j < s.Name.Length; j++)
            {
               
                map[j+1, 1] = s.Name.ToCharArray()[j];
            }
            for (int i = 1; i < Length; i++)
            {
                map[i, 0] = '-';
                map[i, 2] = '-';
            }
            map[0, 0] = '#';
            map[0, map.GetLength(1)-1] = '#';
            map[0, 1] = '|';
            map[map.GetLength(0) - 2, 1] = '|';
            map[map.GetLength(0) - 2, 0] = '#';
            map[map.GetLength(0) - 2, 2] = '#';
            return map;
        }
    }
}
