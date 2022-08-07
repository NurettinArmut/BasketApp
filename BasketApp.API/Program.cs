using BasketApp.Core.Repositories;
using BasketApp.Core.Services;
using BasketApp.Core.UnitOfWork;
using BasketApp.Data;
using BasketApp.Data.Repository;
using BasketApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region MyServices

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<,>), typeof(GenericService<,>));
builder.Services.AddScoped(typeof(IBasketService), typeof(BasketService));

builder.Services.AddSingleton<RedisService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("PostgresSqlServer"), npgsqloption =>
    {
        //BasketApp.Data
        npgsqloption.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});

#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var redisService = app.Services.GetRequiredService<RedisService>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

redisService.Connect();

app.MapControllers();

app.Run();
