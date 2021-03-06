﻿using AutoMapper;

using MVC6Awakens.Models;
using MVC6Awakens.ViewModels.AutoMapper;


namespace MVC6Awakens.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<DomainModelTest, DomainModelTestViewModel>();
        }
    }
}
