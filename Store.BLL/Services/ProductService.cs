using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Abstractions.Services;
using Store.Core.Abstractions.Services.Storage;
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
        private readonly IStorageService _storageService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async Task<ApiResponse> CreateAsync(ProductPostDTO postedProduct)
        {
            Product product = _mapper.Map<Product>(postedProduct);

            if (postedProduct.FormFiles.Count > 0)
            {
                try
                {

                    var results = await _storageService.UploadAsync("product-images", postedProduct.FormFiles);

                    int counter = 0;

                    var productImages = await Task.WhenAll(results.Select(async result =>
                    {
                        counter++;

                        return new ProductImageFile()
                        {
                            IsMain = counter == 1,
                            FileName = Path.GetFileNameWithoutExtension(result.fileName),
                            Extension = Path.GetExtension(result.fileName),
                            Product = product,
                            ImageUrl = await _storageService.GetUploadedFileUrlAsync(result.path, result.fileName)
                        };
                    }));

                    product.ProductImageFiles = productImages;
                }
                catch
                {
                    return new ApiResponse(HttpStatusCode.BadRequest, "Error happened while uploading files.");
                }


                bool isAdded = await _productWriteRepository.AddAsync(product);
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



            bool isDeleted;

            if (soft)
            {
                isDeleted = _productWriteRepository.DeleteSoft(product);

                if (isDeleted)
                {
                    foreach (var image in product.ProductImageFiles)
                    {
                        _productImageFileWriteRepository.DeleteSoft(image);
                    }
                }
            }
            else
            {

                isDeleted = _productWriteRepository.Delete(product);
                if (isDeleted)
                {
                    foreach (var image in product.ProductImageFiles)
                    {
                        _productImageFileWriteRepository.Delete(image);
                        await _storageService.DeleteByUrlAsync(image.ImageUrl);
                    }
                }
            }

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

