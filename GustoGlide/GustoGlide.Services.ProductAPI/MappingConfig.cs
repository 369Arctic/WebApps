using AutoMapper;
using GustoGlide.Services.ProductAPI.Models;
using GustoGlide.Services.ProductAPI.Models.Dto;

namespace GustoGlide.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<ProductDto, Product>();
            });

            return mappingConfig;
        }
    }
}
