using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;
using NorthwindApi.Controllers;
using NorthwindApi.Model.Entity;

namespace NorthwindApi
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CreateCategoryCommand, Category>();
            CreateMap<Category, CreateCategoryCommand>();

            CreateMap<UpdateCategoryCommand, Category>();
            CreateMap<Category, UpdateCategoryCommand>();
        }
    }
}
