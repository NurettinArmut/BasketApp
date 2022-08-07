using BasketApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BasketApp.Data.Seeds
{
    public class ProductSeed : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
            {
                Id = 1,
                Name = "Bilgisayar",
                UnitPrice = 100,
                Stock = 20,
                CreatedDate = DateTime.UtcNow
            }, new Product
            {
                Id = 2,
                Name = "Buzdolabı",
                UnitPrice = 600,
                Stock = 10,
                CreatedDate = DateTime.UtcNow
            }, new Product
            {
                Id = 3,
                Name = "Çamaşır Makinesi",
                UnitPrice = 100,
                Stock = 30,
                CreatedDate = DateTime.UtcNow
            }, new Product
            {
                Id = 4,
                Name = "Elektrikli Süpürge",
                UnitPrice = 2500,
                Stock = 20,
                CreatedDate = DateTime.UtcNow
            }, new Product
            {
                Id = 5,
                Name = "Bulaşık Makinesi",
                UnitPrice = 6600,
                Stock = 40,
                CreatedDate = DateTime.UtcNow
            }
            );
        }
    }
}
