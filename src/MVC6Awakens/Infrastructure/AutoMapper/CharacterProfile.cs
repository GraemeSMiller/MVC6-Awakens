using AutoMapper;
using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.Characters;


namespace MVC6Awakens.Infrastructure.AutoMapper
{
    public class CharacterProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Character, CharacterCreate>().ReverseMap();
        }
    }
}
