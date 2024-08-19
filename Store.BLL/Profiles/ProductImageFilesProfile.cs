using AutoMapper;
using Store.Core.Entities;
using Store.DTO.DTOs.Product;
using Store.DTO.DTOs.ProductImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Profiles
{
    public class ProductImageFilesProfile : Profile
    {
        public ProductImageFilesProfile()
        {
            CreateMap<ProductImageFile, ProductImageFilePostDTO>().ReverseMap();
            CreateMap<ProductImageFile, ProductImageFileGetDTO>().ReverseMap();
        }
    }
}
