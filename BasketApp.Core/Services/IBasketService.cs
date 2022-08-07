using BasketApp.Core.Dtos;
using SharedLibrary.Dtos;

namespace BasketApp.Core.Services
{
    public interface IBasketService
    {
        Task<Response<BasketStockDto>> GetProductDto(int productId);
        Task<List<ProductDto>> GetAllProducts();
    }
}
