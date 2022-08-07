using BasketApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace BasketApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;       
        public BasketsController(IBasketService basketService) 
        {
            _basketService = basketService;
        }

        [HttpGet("[action]/{productId}")]
        public async Task<IActionResult> AddBasketFromProduct(int productId)
        {
            return ActionResultInstance(await _basketService.GetProductDto(productId));
        }
    }
}
