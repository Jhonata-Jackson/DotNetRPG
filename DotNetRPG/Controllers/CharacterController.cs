using DotNetRPG.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController: ControllerBase
    {
        private static Character Knight = new Character();
        
        [HttpGet]
        public ActionResult<Character> Get()
        {
            return Ok(Knight);
        }
    }
}

