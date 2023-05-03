using CryptoTestApi;
using CryptoTestApi.BackgroundServices;
using CryptoTestApi.Database;
using CryptoTestApi.Database.Interfaces;
using CryptoTestApi.Database.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSingleton<Cashed>();

builder.Services.AddHostedService<CheckBalancesService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CryptoDbContext>(opt =>
{
    opt.UseNpgsql(config.GetConnectionString("Default"));
});

builder.Services.AddTransient<IWalletRepository, WalletsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
