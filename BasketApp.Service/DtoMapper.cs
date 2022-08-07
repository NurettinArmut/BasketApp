using AutoMapper;
using BasketApp.Core.Dtos;
using BasketApp.Core.Models;
using SharedLibrary.Dtos;

namespace BasketApp.Service
{
    internal class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<ProductDto, Product>().ReverseMap(); 
            CreateMap<ProductDto, BasketStockDto>().ReverseMap(); 
        }
    }
}
