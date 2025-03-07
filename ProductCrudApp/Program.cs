using Microsoft.EntityFrameworkCore;
using ProductCrudApp.Data;
using ProductCrudApp.Repository;
using ProductCrudApp.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var mySqlConnectionStr = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(mySqlConnectionStr, ServerVersion.AutoDetect(mySqlConnectionStr)));

builder.Services.AddTransient<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddTransient<IProductRepository, ProductRepositoryImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<IProductService, ProductServiceImpl>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


public partial class Program { }

