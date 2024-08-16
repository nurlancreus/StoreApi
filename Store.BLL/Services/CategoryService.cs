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
        private readonly ICategoryReadRepository _readRepository;
        private readonly ICategoryWriteRepository _writeRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryReadRepository readRepository, ICategoryWriteRepository writeRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse> CreateAsync(CategoryPostDTO postedCategory)
        {
            Category category = _mapper.Map<Category>(postedCategory);

            bool isAdded = await _writeRepository.AddAsync(category);

            if (isAdded)
            {
                await _writeRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.Created);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponse> DeleteAsync(string id, bool soft = true)
        {
            Category? category = await _readRepository.GetByIdAsync(id);

            if (category == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            bool isDeleted = soft
                ? _writeRepository.DeleteSoft(category)
                : _writeRepository.Delete(category);

            if (isDeleted)
            {
                await _writeRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.NoContent);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }

        public async Task<ApiResponseWithData<ICollection<CategoryGetDTO>>> GetAllAsync()
        {
            var query = _readRepository.GetAll(false);

            List<Category> categories = await query.ToListAsync();

            ICollection<CategoryGetDTO> categoryGetDTOs = _mapper.Map<ICollection<CategoryGetDTO>>(categories);

            return new ApiResponseWithData<ICollection<CategoryGetDTO>>(HttpStatusCode.OK, categoryGetDTOs);
        }

        public async Task<ApiResponseWithData<CategoryGetDTO>> GetByIdAsync(string id)
        {

            Category? category = await _readRepository.GetByIdAsync(id, false);

            if (category == null)
            {
                return new ApiResponseWithData<CategoryGetDTO>(HttpStatusCode.NotFound, null);
            }

            CategoryGetDTO categoryGetDTO = _mapper.Map<CategoryGetDTO>(category);

            return new ApiResponseWithData<CategoryGetDTO>(HttpStatusCode.OK, categoryGetDTO);
        }

        public async Task<ApiResponse> UpdateAsync(string id, CategoryPostDTO updatedCategory)
        {
            Category? category = await _readRepository.GetByIdAsync(id);

            if (category == null)
            {
                return new ApiResponse(HttpStatusCode.NotFound);
            }

            category.Name = updatedCategory.Name;
            bool isUpdated = _writeRepository.Update(category);

            if (isUpdated)
            {
                await _writeRepository.SaveChangesAsync();
                return new ApiResponse(HttpStatusCode.OK);
            }
            else
            {
                return new ApiResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
