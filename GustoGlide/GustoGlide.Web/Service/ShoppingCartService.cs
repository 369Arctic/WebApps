using GustoGlide.Web.Models;
using GustoGlide.Web.Service.IService;
using GustoGlide.Web.Utility;

namespace GustoGlide.Web.Service
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IBaseService _baseService;
        public ShoppingCartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = cartDto,
                Url = StaticDetails.ShoppingCartApiBase + "/api/cart/ApplyCoupon"
            });
        }


        public async Task<ResponseDto?> GetCartByUserIdAsync(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ShoppingCartApiBase + "/api/cart/GetCart/" + userId
            });
        }

        

        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDerailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = cartDerailsId,
                Url = StaticDetails.ShoppingCartApiBase + "/api/cart/RemoveCart"
            });
        }


        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = cartDto,
                Url = StaticDetails.ShoppingCartApiBase + "/api/cart/CartUpsert"
            });
        }
    }
}
