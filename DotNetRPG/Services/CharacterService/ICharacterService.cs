using DotNetRPG.Dtos;
using DotNetRPG.Models;

namespace DotNetRPG.Services.CharacterService;

public interface ICharacterService
{
    Task<ServiceResponse<List<GetCharacterDTO>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter);
    Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter);
    
}