using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Store.Core.Abstractions.Repositories;
using Store.Core.Abstractions.Services;
using Store.Core.Entities;
using Store.Core.Responses;
using Store.DAL.Repositories;
using Store.DTO.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryReadRepository _categoryReadRepository;
        private readonly ICategoryWriteRepository _categoryWriteRepository;
        private readonly IProductReadRepository _productReadRepository;
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository categoryReadRepository, ICategoryWriteRepository categoryWriteRepository, IProductReadRepository productReadRepository, IProductWriteRepository productWriteRepository, IMapper mapper)
        {
            _categoryReadRepository = categoryReadRepository;
            _categoryWriteRepository = categoryWriteRepository;
            _productReadRepository = productReadRepository;
            _productWriteRepository = productWriteRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(CategoryPostDTO postedCategory)
        {
            Category category = _mapper.Map<Category>(postedCategory);

            bool isAdded = await _categoryWriteRepository.AddAsync(category);

            if (isAdded)
            {
                await _categoryWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.Created);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponse> DeleteAsync(string id, bool soft = true)
        {
            Category? category = await _categoryReadRepository.GetByIdAsync(id);

            if (category == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            bool isDeleted = soft
                ? _categoryWriteRepository.DeleteSoft(category)
                : _categoryWriteRepository.Delete(category);

            if (isDeleted)
            {
                await _categoryWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponseWithData<ICollection<CategoryGetDTO>>> GetAllAsync()
        {
            var query = _categoryReadRepository.GetAll(false).Include(c => c.Products).ThenInclude(p => p.ProductImageFiles);

            List<Category> categories = await query.ToListAsync();

            ICollection<CategoryGetDTO> categoryGetDTOs = _mapper.Map<ICollection<CategoryGetDTO>>(categories);

            return new ApiResponseWithData<ICollection<CategoryGetDTO>>(HttpStatusCode.OK, categoryGetDTOs);
        }

        public async Task<ApiResponseWithData<CategoryGetDTO>> GetByIdAsync(string id)
        {

            Category? category = await _categoryReadRepository.GetByIdAsync(id, false);

            if (category == null)
            {
                return new ApiResponseWithData<CategoryGetDTO>(HttpStatusCode.NotFound, null);
            }

            await _categoryReadRepository.Table.Entry(category).Collection(c => c.Products).LoadAsync();

            // For each product, explicitly load the associated ProductImages
            foreach (var product in category.Products)
            {
                await _productReadRepository.Table.Entry(product)
                    .Collection(p => p.ProductImageFiles)
                    .LoadAsync();
            }

            CategoryGetDTO categoryGetDTO = _mapper.Map<CategoryGetDTO>(category);

            return new ApiResponseWithData<CategoryGetDTO>(HttpStatusCode.OK, categoryGetDTO);
        }

        public async Task<ApiResponse> UpdateAsync(string id, CategoryPostDTO updatedCategory)
        {
            Category? category = await _categoryReadRepository.GetByIdAsync(id);

            if (category == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            category.Name = updatedCategory.Name;
            bool isUpdated = _categoryWriteRepository.Update(category);

            if (isUpdated)
            {
                await _categoryWriteRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.OK);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
