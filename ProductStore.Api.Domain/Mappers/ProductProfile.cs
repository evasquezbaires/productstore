using AutoMapper;
using ProductStore.Api.Domain.Entities;
using ProductStore.Api.Model;

namespace ProductStore.Api.Domain.Mappers
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductRead>()
                .ForMember(dest => dest.StatusName, opt => opt.MapFrom(src => src.StatusCode.ToString()));
            CreateMap<ProductWrite, Product>();
            CreateMap<ProductUpdate, Product>();
        }
    }
}
