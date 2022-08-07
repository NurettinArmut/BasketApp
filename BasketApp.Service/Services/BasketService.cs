using BasketApp.Core.Dtos;
using BasketApp.Core.Models;
using BasketApp.Core.Services;
using SharedLibrary.Dtos;
using SharedLibrary.Services;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace BasketApp.Service.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;
        private readonly IGenericService<Product, ProductDto> _genericService;

        private readonly IDatabase db;
        private readonly IDatabase db2;

        private string listBasketKey = "basket2"; // "basketDto";
        private string listProductsKey = "ProductList2"; // "Products";

        BasketStockDto bBasketDto = new();

        public BasketService(RedisService redisService, IGenericService<Product, ProductDto> genericService)
        {
            _redisService = redisService;
            _genericService = genericService;

            db = _redisService.GetDb(1);
            db2 = _redisService.GetDb(2);
        }

        public async Task<Response<BasketStockDto>> GetProductDto(int productId)
        {
            await GetAllProducts();

            //okuma
            if (db.KeyExists(listProductsKey))
            {
                int countBasket = 0;
                var productDtoList = db.ListRange(listProductsKey).ToList();

                Byte[] byteProductDto = productDtoList.SelectMany(a => Encoding.UTF8.GetBytes(a)).ToArray();

                string jsonproduct = Encoding.UTF8.GetString(byteProductDto);

                List<ProductDto> pDtos = JsonSerializer.Deserialize<List<ProductDto>>(jsonproduct);

                var pDto = pDtos.FirstOrDefault(p => p.Id == productId);

                listBasketKey += pDto.Id.ToString();

                var value = db2.StringGet(listBasketKey);

                if (!value.HasValue)
                {
                    db2.StringSet(listBasketKey, pDto.Stock);
                }

                //db.KeyDelete(listBasketKey);
                //countBasket = Convert.ToInt32(db2.StringGet(listBasketKey));

                db.KeyExpire(listBasketKey, DateTime.Now.AddMinutes(5)); //todo dk artırılacak

                db2.StringIncrement(listBasketKey, 1);
               // db2.StringDecrement(listBasketKey, 1);
                countBasket =  Convert.ToInt32(db2.StringGet(listBasketKey));

                //if (Convert.ToInt32(db2.StringGet(listBasketKey) == 0)
                //{
                //    db2.StringIncrement(listBasketKey, 1);
                //    countBasket = Convert.ToInt32(db2.StringGet(listBasketKey));
                //}

                bBasketDto.Id = pDto.Id;
                bBasketDto.Name = pDto.Name;
                bBasketDto.BasketStock = Convert.ToInt32(countBasket);
                bBasketDto.ProductStock = pDto.Stock - Convert.ToInt32(countBasket);

                if (pDto.Stock - Convert.ToInt32(db2.StringGet(listBasketKey)) <= 0)
                {
                    bBasketDto.BasketStock = pDto.Stock;
                    bBasketDto.ProductStock = 0;
                    return Response<BasketStockDto>.Fail("Stokta ürünümüz kalmamıştır.", 404, true);
                }
            }

            return Response<BasketStockDto>.Success(ObjectMapper.Mapper.Map<BasketStockDto>(bBasketDto), 200);
        }


        public async Task<List<ProductDto>> GetAllProducts()
        {

            List<ProductDto> productsDto = new();
            if (!db.KeyExists(listProductsKey))
            {
                var products = await _genericService.GetAllListAsync();
                productsDto = ObjectMapper.Mapper.Map<List<ProductDto>>(products);

                db.KeyExpire(listProductsKey, DateTime.Now.AddMinutes(10)); //todo dk artırılacak

                if (products != null)
                {
                    string jsonProductsDto = JsonSerializer.Serialize(productsDto);

                    Byte[] byteProductsDto = Encoding.UTF8.GetBytes(jsonProductsDto);

                    //  db.ListRemove(listProductsKey, byteProductsDto);

                    //Redis Cache ekleme
                    db.ListRightPush(listProductsKey, byteProductsDto);
                }
            }

            return productsDto;
        }
    }
}
