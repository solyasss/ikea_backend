using ikea_backend.Data;
using ikea_business.DTO;
using ikea_business.Helpers;
using ikea_data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ---------- services ----------
builder.Services.AddDbContext<IkeaDbContext>(options =>
{
    var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connStr);
});

builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ---------- middleware ----------
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ---------- API ----------
app.MapGet("/api/categories", async (IkeaDbContext db) =>
{
    var list = await db.Categories
        .Include(c => c.Children)
        .Select(c => new
        {
            c.Id, c.ParentId, c.Title, c.Slug,
            Children = c.Children.Select(sc => new { sc.Id, sc.ParentId, sc.Title, sc.Slug })
        })
        .Where(x => x.ParentId == null)
        .ToListAsync();

    return Results.Ok(list);
});

app.MapGet("/api/categories/{id:int}", async (int id, IkeaDbContext db) =>
{
    var cat = await db.Categories.Include(c => c.Children).FirstOrDefaultAsync(c => c.Id == id);
    if (cat == null) return Results.NotFound();
    return Results.Ok(new
    {
        cat.Id, cat.ParentId, cat.Title, cat.Slug,
        Children = cat.Children.Select(sc => new { sc.Id, sc.ParentId, sc.Title, sc.Slug })
    });
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
    var cat = await db.Categories.Include(c => c.Children).FirstOrDefaultAsync(c => c.Id == id);
    if (cat == null) return Results.NotFound();
    db.Categories.Remove(cat);
    await db.SaveChangesAsync();
    return Results.Ok(cat);
});


app.MapGet("/api/users", async (IkeaDbContext db) =>
{
    var list = await db.Users
        .Select(u => new
        {
            u.Id,
            u.IsAdmin,
            u.FirstName,
            u.LastName,
            u.BirthDate,
            u.Country,
            u.Address,
            u.Phone,
            u.Email
        })
        .ToListAsync();

    return Results.Ok(list);
});

app.MapGet("/api/users/{id:int}", async (int id, IkeaDbContext db) =>
{
    var u = await db.Users.FindAsync(id);
    if (u == null) return Results.NotFound();
    return Results.Ok(new
    {
        u.Id,
        u.IsAdmin,
        u.FirstName,
        u.LastName,
        u.BirthDate,
        u.Country,
        u.Address,
        u.Phone,
        u.Email
    });
});

app.MapPost("/api/users", async (UserInput input, IkeaDbContext db) =>
{
    PasswordHasher.CreateHash(input.Password, out var hash, out var salt);

    var u = new User
    {
        IsAdmin      = input.IsAdmin,
        FirstName    = input.FirstName,
        LastName     = input.LastName,
        BirthDate    = input.BirthDate,
        Country      = input.Country,
        Address      = input.Address,
        Phone        = input.Phone,
        Email        = input.Email,
        PasswordHash = hash,
        PasswordSalt = salt
    };

    db.Users.Add(u);
    await db.SaveChangesAsync();
    return Results.Created($"/api/users/{u.Id}", new { u.Id });
});

app.MapPut("/api/users/{id:int}", async (int id, UserInput input, IkeaDbContext db) =>
{
    var u = await db.Users.FindAsync(id);
    if (u == null) return Results.NotFound();

    u.IsAdmin   = input.IsAdmin;
    u.FirstName = input.FirstName;
    u.LastName  = input.LastName;
    u.BirthDate = input.BirthDate;
    u.Country   = input.Country;
    u.Address   = input.Address;
    u.Phone     = input.Phone;
    u.Email     = input.Email;

    PasswordHasher.CreateHash(input.Password, out var hash, out var salt);
    u.PasswordHash = hash;
    u.PasswordSalt = salt;

    await db.SaveChangesAsync();
    return Results.Ok(new { u.Id });
});

app.MapDelete("/api/users/{id:int}", async (int id, IkeaDbContext db) =>
{
    var u = await db.Users.FindAsync(id);
    if (u == null) return Results.NotFound();
    db.Users.Remove(u);
    await db.SaveChangesAsync();
    return Results.Ok(new { u.Id });
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IkeaDbContext>();
    db.Database.Migrate();
}

app.Run();
