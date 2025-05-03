using ikea_backend.Data;
using ikea_backend.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<IkeaDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connStr);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(); // добавил CORS

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/categories", async (IkeaDbContext db) =>
{
    var list = await db.Categories
        .Include(c => c.Children)
        .Select(c => new
        {
            c.Id,
            c.ParentId,
            c.Title,
            c.Slug,
            Children = c.Children.Select(sc => new
            {
                sc.Id,
                sc.ParentId,
                sc.Title,
                sc.Slug
            })
        })
        .Where(x => x.ParentId == null)
        .ToListAsync();
    return Results.Ok(list);
});

app.MapGet("/api/categories/{id:int}", async (int id, IkeaDbContext db) =>
{
    var cat = await db.Categories
        .Include(c => c.Children)
        .FirstOrDefaultAsync(c => c.Id == id);
    if (cat == null) return Results.NotFound();
    var result = new
    {
        cat.Id,
        cat.ParentId,
        cat.Title,
        cat.Slug,
        Children = cat.Children.Select(sc => new
        {
            sc.Id,
            sc.ParentId,
            sc.Title,
            sc.Slug
        })
    };
    return Results.Ok(result);
});

app.MapPost("/api/categories", async (Category input, IkeaDbContext db) =>
{
    db.Categories.Add(input);
    await db.SaveChangesAsync();
    return Results.Created($"/api/categories/{input.Id}", input);
});

app.MapPut("/api/categories/{id:int}", async (int id, Category input, IkeaDbContext db) =>
{
    var cat = await db.Categories.FindAsync(id);
    if (cat == null) return Results.NotFound();
    cat.Title = input.Title;
    cat.Slug = input.Slug;
    cat.ParentId = input.ParentId;
    await db.SaveChangesAsync();
    return Results.Ok(cat);
});

app.MapDelete("/api/categories/{id:int}", async (int id, IkeaDbContext db) =>
{
    var cat = await db.Categories
        .Include(c => c.Children)
        .FirstOrDefaultAsync(c => c.Id == id);
    if (cat == null) return Results.NotFound();
    db.Categories.Remove(cat);
    await db.SaveChangesAsync();
    return Results.Ok(cat);
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IkeaDbContext>(); 
    db.Database.Migrate();
}

app.Run(); 
