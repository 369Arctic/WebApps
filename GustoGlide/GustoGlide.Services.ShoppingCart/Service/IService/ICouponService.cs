using GustoGlide.Services.ShoppingCartAPI.Models.Dto;

namespace GustoGlide.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
