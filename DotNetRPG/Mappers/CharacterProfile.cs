using AutoMapper;
using DotNetRPG.Dtos;
using DotNetRPG.Models;

namespace DotNetRPG.Mappers;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, GetCharacterDto>();
        CreateMap<AddCharacterDto, Character>();
    }
}