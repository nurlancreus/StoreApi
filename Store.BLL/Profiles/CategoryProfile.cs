using AutoMapper;
using Store.Core.Entities;
using Store.DTO.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryPostDTO>().ReverseMap();
            CreateMap<Category, CategoryGetDTO>().ReverseMap();
        }
    }
}
