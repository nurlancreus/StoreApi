using Store.Core.Responses;
using Store.DTO.DTOs.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Abstractions.Services
{
    public interface ICategoryService
    {
        Task<ApiResponse> CreateAsync(CategoryPostDTO postedCategory);
        Task<ApiResponse> DeleteAsync(string id, bool soft = true);
        Task<ApiResponse> UpdateAsync(string id, CategoryPostDTO updatedCategory);
        Task<ApiResponseWithData<CategoryGetDTO>> GetByIdAsync(string id);
        Task<ApiResponseWithData<ICollection<CategoryGetDTO>>> GetAllAsync();
    }
}
