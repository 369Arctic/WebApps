using GustoGlide.Services.ShoppingCartAPI.Models.Dto;

namespace GustoGlide.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
