using DotNetRPG.Dtos;
using DotNetRPG.Models;
using DotNetRPG.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController: ControllerBase
    {
        
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }
        
        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDTO>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDTO>>> GetSingle(int id)
        {   
            return Ok(await _characterService.GetCharacterById(id));
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CreateCharacterDTO>>>> AddCharacter(CreateCharacterDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }
    }
}

