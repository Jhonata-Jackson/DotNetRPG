using AutoMapper;
using DotNetRPG.Dtos;
using DotNetRPG.Models;

namespace DotNetRPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private ICharacterService _characterServiceImplementation;
    
    private static List<Character> characters = new List<Character>() {
        new Character(),
        new Character { Id = 1, Name = "Sam" }
    };

    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDTO >>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();
        
        var character = characters.FirstOrDefault(c => c.Id == id);
        serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDTO>>> AddCharacter(AddCharacterDTO newCharacter)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDTO>>();
        var character = _mapper.Map<Character>(newCharacter);

        character.Id = characters.Max(c => c.Id) + 1;
        characters.Add(character);
        serviceResponse.Data = characters.Select(c => _mapper.Map<GetCharacterDTO>(c)).ToList();
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDTO>> UpdateCharacter(UpdateCharacterDTO updatedCharacter)
    {   
        var serviceResponse = new ServiceResponse<GetCharacterDTO>();
        
        try
        {
            var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

            if (character is null)
            {
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
            }

            character.Name = updatedCharacter.Name;
            character.HitPoints = updatedCharacter.HitPoints;
            character.Strength = updatedCharacter.Strength;
            character.Defense = updatedCharacter.Defense;
            character.Intelligence = updatedCharacter.Intelligence;
            character.Class = updatedCharacter.Class;

            serviceResponse.Data = _mapper.Map<GetCharacterDTO>(character);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
}