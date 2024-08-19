using Store.Core.Responses;
using Store.DTO.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Abstractions.Services
{
    public interface IProductService
    {
        Task<ApiResponse> CreateAsync(ProductPostDTO postedProduct);
        Task<ApiResponse> DeleteAsync(string id, bool soft = true);
        Task<ApiResponse> UpdateAsync(string id, ProductPostDTO updatedProduct);
        Task<ApiResponseWithData<ProductGetDTO>> GetByIdAsync(string id);
        Task<ApiResponseWithData<ICollection<ProductGetDTO>>> GetAllAsync();
    }
}
