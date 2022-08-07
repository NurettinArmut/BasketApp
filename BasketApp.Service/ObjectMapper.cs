using AutoMapper;

namespace BasketApp.Service
{
    public static class ObjectMapper
    {
        // Lazy Loading 
        //Sadece ihtiyaç duyulduğunda memory'e data yüklesin ..
        private static readonly Lazy<IMapper> lazy = new(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<DtoMapper>();
            });
            return config.CreateMapper();
        });

        public static IMapper Mapper => lazy.Value;
    }
}
