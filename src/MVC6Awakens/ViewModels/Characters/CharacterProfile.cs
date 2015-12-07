using AutoMapper;
using MVC6Awakens.Models;

namespace MVC6Awakens.ViewModels.Characters
{
    public class CharacterProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Character, CharacterPublish>().ReverseMap();
            Mapper.CreateMap<Character, CharacterEdit>().ReverseMap();
            Mapper.CreateMap<Character, CharacterCreate>().ReverseMap();
            Mapper.CreateMap<Character, CharacterDetail>();
        }
    }
}
