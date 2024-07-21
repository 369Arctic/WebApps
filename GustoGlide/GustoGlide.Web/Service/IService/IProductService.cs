using GustoGlide.Web.Models;

namespace GustoGlide.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int productId );
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto );
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProdyctAsync(int productId);

    }
}
