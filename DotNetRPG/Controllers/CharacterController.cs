using DotNetRPG.Dtos;
using DotNetRPG.Models;
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
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
        {
            var response = await _characterService.GetAllCharacters();
            return  response.Data is null ?  NotFound(response) : Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSingle(int id)
        {   
            var response = await _characterService.GetCharacterById(id);
            return  response.Data is null ?  NotFound(response) : Ok(response);
        }
        
        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = await _characterService.AddCharacter(newCharacter);
            return  response.Data is null ?  NotFound(response) : Ok(response);
        }
        
        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            return response.Data is null ?  NotFound(response) : Ok(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);

            return response.Data is null ?  NotFound(response) : Ok(response);
        }
    }
}

