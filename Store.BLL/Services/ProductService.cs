using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Abstractions.Services;
using Store.Core.Entities;
using Store.Core.Responses;
using Store.DTO.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(ProductPostDTO postedProduct)
        {
            Product product = _mapper.Map<Product>(postedProduct);

            bool isAdded = await _productWriteRepository.AddAsync(product);

            if (isAdded)
            {
                await _productWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.Created);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponse> DeleteAsync(string id, bool soft = true)
        {
            Product? product = await _productReadRepository.GetByIdAsync(id);

            if (product == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            bool isDeleted = soft
                ? _productWriteRepository.DeleteSoft(product)
                : _productWriteRepository.Delete(product);

            if (isDeleted)
            {
                await _productWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponseWithData<ICollection<ProductGetDTO>>> GetAllAsync()
        {
            var query = _productReadRepository.GetAll(false);

            List<Product> products = await query.Include(p => p.Categories).Include(p => p.ProductImageFiles).ToListAsync();

            ICollection<ProductGetDTO> productGetDTOs = _mapper.Map<ICollection<ProductGetDTO>>(products);

            return new ApiResponseWithData<ICollection<ProductGetDTO>>(HttpStatusCode.OK, productGetDTOs);
        }

        public async Task<ApiResponseWithData<ProductGetDTO>> GetByIdAsync(string id)
        {

            Product? product = await _productReadRepository.GetByIdAsync(id, false);

            if (product == null)
            {
                return new ApiResponseWithData<ProductGetDTO>(HttpStatusCode.NotFound, null);
            }

            await _productReadRepository.Table.Entry(product).Collection(p => p.ProductImageFiles).LoadAsync();

            await _productReadRepository.Table.Entry(product).Collection(p => p.Categories).LoadAsync();

            ProductGetDTO productGetDTO = _mapper.Map<ProductGetDTO>(product);

            return new ApiResponseWithData<ProductGetDTO>(HttpStatusCode.OK, productGetDTO);
        }

        public async Task<ApiResponse> UpdateAsync(string id, ProductPostDTO updatedProduct)
        {
            Product? Product = await _productReadRepository.GetByIdAsync(id);

            if (Product == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            Product.Name = updatedProduct.Name;
            bool isUpdated = _productWriteRepository.Update(Product);

            if (isUpdated)
            {
                await _productWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.OK);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}

