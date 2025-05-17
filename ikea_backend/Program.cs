using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using ikea_business.Mapping;
using ikea_business.Services;
using ikea_business.Validation;
using ikea_business.Services.Interfaces;
using ikea_data.Data;
using ikea_data.Repositories.Implementations;
using ikea_data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using ikea_business.Services.Implementations;
using ikea_data.Models;

var builder = WebApplication.CreateBuilder(args);

// DbContext
builder.Services.AddDbContext<IkeaDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connStr);
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductInputValidator>();

// Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<INewArrivalRepository, NewArrivalRepository>();
builder.Services.AddScoped<ISetRepository, SetRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IProductCommentRepository, ProductCommentRepository>();
builder.Services.AddScoped<IProductCharacteristicRepository, ProductCharacteristicRepository>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<INewArrivalService, NewArrivalService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IProductCommentService, ProductCommentService>();
builder.Services.AddScoped<IProductCharacteristicService, ProductCharacteristicService>();


// CORS
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});


// Swagger
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers(); 

app.Run();