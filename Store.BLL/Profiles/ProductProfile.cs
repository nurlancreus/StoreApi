using AutoMapper;
using Store.Core.Entities;
using Store.DTO.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductPostDTO>().ReverseMap();
            CreateMap<Product, ProductGetDTO>().ReverseMap();
        }
    }
}
