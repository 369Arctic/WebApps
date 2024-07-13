using GustoGlide.Web.Models;
using GustoGlide.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GustoGlide.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? coupons = [];

            ResponseDto responseDto = await _couponService.GetAllCouponsAsync();

            if (responseDto != null && responseDto.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Result));
            }
            
            return View(coupons);
        }
    }
}
