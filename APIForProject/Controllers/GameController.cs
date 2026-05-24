using Microsoft.AspNetCore.Mvc;
using DBL;
using Models;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc.Routing;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APIForProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class GameController : ControllerBase
    {

        [HttpPost("GetAllSkills")]
        public async Task<List<Skill>> GetListOfSkills()
        {
            SkillDB skillDB = new SkillDB();
            List<Skill> skills =  await skillDB.GetAllAsync();
            
            
            return skills ;
        }//gets the list of all of the skills

        [HttpPost("GetUserById")]
        public async Task<Consumer> GetUserByID([FromBody]int id)
        {
            ConsumerDB cDB = new ConsumerDB();
             Consumer c = await cDB.GetByIdAsync(id);
             if(c == null)
            {
                return null;
            }
            return c;
        }//gets a user by id

        public record logindata(string username, string password);
        public record returninfo( Consumer user, string response);
        [HttpPost("Login")]
        public async Task<returninfo> LoginGame([FromBody]logindata loginData)
        {
            ConsumerDB CDB = new ConsumerDB();
           var c =await CDB.LoginAsync(loginData.username,loginData.password);
            returninfo info = new returninfo(c.Item1,c.Item2);
            return info;
        }//login for the game

        // POST api/<GameController>
        [HttpPost("GetPlayerHp")]
        public async Task<int>  HpPlayerGet([FromBody] string value)
        {
            return 100;
            
        }

        [HttpPost("GetEnemyData")]

        public async Task<Enemies> GetEnemyData([FromBody] int value)
        {
            EnemiesDB eDB = new EnemiesDB();
            List<Enemies> enemies = await eDB.GetByIdAsync(value);
            return enemies[0];

        }



    }
}
