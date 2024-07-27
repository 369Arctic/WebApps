using GustoGlide.Web.Models;

namespace GustoGlide.Web.Service.IService
{
    public interface IShoppingCartService
    {
        Task<ResponseDto?> GetCartByUserIdAsync(string userId);
        Task<ResponseDto?> UpsertCartAsync(CartDto cartDto);
        Task<ResponseDto?> RemoveFromCartAsync(int cartDerailsId);
        Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto);
    }
}
