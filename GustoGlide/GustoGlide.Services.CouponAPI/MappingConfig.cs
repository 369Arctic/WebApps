using AutoMapper;
using GustoGlide.Services.CouponAPI.Models;
using GustoGlide.Services.CouponAPI.Models.Dto;

namespace GustoGlide.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
