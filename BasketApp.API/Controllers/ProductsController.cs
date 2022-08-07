using BasketApp.Core.Dtos;
using BasketApp.Core.Models;
using BasketApp.Core.Services;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary.Services;
using StackExchange.Redis;

namespace BasketApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IGenericService<Product, ProductDto> _genericService;
       
        public ProductsController(IGenericService<Product, ProductDto> genericService) 
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResultInstance(await _genericService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            return ActionResultInstance(await _genericService.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto ProductDto)
        {
            return ActionResultInstance(await _genericService.AddAsync(ProductDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto ProductDto)
        {
            return ActionResultInstance(await _genericService.UpdateAsync(ProductDto, ProductDto.Id));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            return ActionResultInstance(await _genericService.Remove(id));
        }
    }
}
