using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.Dtos;
using DotNetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.Services.CharacterService;

public class CharacterService : ICharacterService
{
    private readonly IMapper _mapper;
    private readonly DataContext _dataContext;

    public CharacterService(IMapper mapper, DataContext dataContext)
    {
        _mapper = mapper;
        _dataContext = dataContext;
    }
    
    public async Task<ServiceResponse<List<GetCharacterDto >>> GetAllCharacters()
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        var dbCharacters = await _dataContext.Characters.ToListAsync();
        serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
    {
        var serviceResponse = new ServiceResponse<GetCharacterDto>();

        try
        {
            var dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (dbCharacter is null)
            {
                throw new Exception($"Character with Id '{id}' not found.");
            }
        
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {   
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        
        try
        {
            var character = _mapper.Map<Character>(newCharacter);
            var dbCharacters = await _dataContext.Characters.ToListAsync();

            character.Id = dbCharacters.Max(c => c.Id) + 1;
            
           await _dataContext.Characters.AddAsync(character);
           await _dataContext.SaveChangesAsync();
           
           var newCharacterList = await _dataContext.Characters.ToListAsync();

            serviceResponse.Data = _mapper.Map<List<GetCharacterDto>?>(newCharacterList);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {   
        var serviceResponse = new ServiceResponse<GetCharacterDto>();
        
        try
        {
            var dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);

            if (dbCharacter is null)
            {
                throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");
            }

            _mapper.Map(updatedCharacter, dbCharacter);

            dbCharacter.Name = updatedCharacter.Name;
            dbCharacter.HitPoints = updatedCharacter.HitPoints;
            dbCharacter.Strength = updatedCharacter.Strength;
            dbCharacter.Defense = updatedCharacter.Defense;
            dbCharacter.Intelligence = updatedCharacter.Intelligence;
            dbCharacter.Class = updatedCharacter.Class;
            
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
        var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
        
        try
        {
            var dbCharacter = await _dataContext.Characters.FirstOrDefaultAsync(c => c.Id == id);

            if (dbCharacter is null)
            {
                throw new Exception($"Character with Id '{id}' not found.");
            }

            _dataContext.Characters.Remove(dbCharacter);
            await _dataContext.SaveChangesAsync();

            serviceResponse.Data = _dataContext.Characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
        }
        catch (Exception ex)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = ex.Message;
        }
        
        return serviceResponse;
    }
}