using ikea_data.Models;
using Microsoft.EntityFrameworkCore;

namespace ikea_backend.Data;

public class IkeaDbContext : DbContext
{
    public IkeaDbContext(DbContextOptions<IkeaDbContext> options) : base(options) { }

    public DbSet<Category> Categories            => Set<Category>();
    public DbSet<Product> Products               => Set<Product>();
    public DbSet<ProductCharacteristic> ProductCharacteristics => Set<ProductCharacteristic>();
    public DbSet<ProductImage> ProductImages     => Set<ProductImage>();
    public DbSet<ProductComment> ProductComments => Set<ProductComment>();
    public DbSet<Set> Sets                       => Set<Set>();
    public DbSet<SetItem> SetItems               => Set<SetItem>();
    public DbSet<User> Users                     => Set<User>();   
    public DbSet<NewArrival> NewArrivals => Set<NewArrival>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ---------- связи ----------
        modelBuilder.Entity<Category>()
            .HasMany(c => c.Children)
            .WithOne(c => c.Parent)
            .HasForeignKey(c => c.ParentId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany()
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        

        modelBuilder.Entity<ProductCharacteristic>()
            .HasOne(pc => pc.Product)
            .WithMany()
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductImage>()
            .HasOne(pi => pi.Product)
            .WithMany()
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductComment>()
            .HasOne(pc => pc.Product)
            .WithMany()
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SetItem>()
            .HasOne(si => si.Set)
            .WithMany()
            .HasForeignKey(si => si.SetId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SetItem>()
            .HasOne(si => si.Product)
            .WithMany()
            .HasForeignKey(si => si.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
        

        // ---------- сиды (как у вас было) ----------
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Title = "Гостиная", Slug = "living-room" },
            new Category { Id = 2, Title = "Спальня", Slug = "bedroom" },
            new Category { Id = 3, Title = "Кухня и столовая", Slug = "kitchen-and-dining" },
            new Category { Id = 4, Title = "Ванная", Slug = "bathroom" },
            new Category { Id = 5, Title = "Офис", Slug = "office" }
        );
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 6, ParentId = 1, Title = "Диваны", Slug = "sofas" },           // … и т. д.
            new Category { Id = 30, ParentId = 5, Title = "Настольные лампы", Slug = "desk-lamps" }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, CategoryId = 19, Name = "Chair", Slug = "chair-1", Price = 59.99m, MainImage = "/src/assets/img/products/product-1.png" },
            new Product { Id = 2, CategoryId = 12, Name = "Lamp",  Slug = "lamp-2",  Price = 19.99m, MainImage = "/src/assets/img/products/product-2.png" },
            new Product { Id = 3, CategoryId = 12, Name = "Lamp",  Slug = "lamp-3",  Price = 19.99m, MainImage = "/src/assets/img/products/product-3.png" }
        );

        modelBuilder.Entity<ProductImage>().HasData(
            new ProductImage { Id = 1, ProductId = 1, ImageUrl = "/src/assets/img/products/product-1.png", SortOrder = 0 },
            new ProductImage { Id = 2, ProductId = 2, ImageUrl = "/src/assets/img/products/product-2.png", SortOrder = 0 },
            new ProductImage { Id = 3, ProductId = 3, ImageUrl = "/src/assets/img/products/product-3.png", SortOrder = 0 }
        );

        modelBuilder.Entity<ProductComment>().HasData(
            new ProductComment { Id = 1, ProductId = 1, UserName = "User1", CommentText = "Nice chair", Rating = 5 },
            new ProductComment { Id = 2, ProductId = 2, UserName = "User2", CommentText = "Bright lamp", Rating = 4 }
        );

        modelBuilder.Entity<ProductCharacteristic>().HasData(
            new ProductCharacteristic { Id = 1, ProductId = 1, Name = "Material", Value = "Wood" },
            new ProductCharacteristic { Id = 2, ProductId = 1, Name = "Color",    Value = "Brown" },
            new ProductCharacteristic { Id = 3, ProductId = 2, Name = "Color",    Value = "White" }
        );

        modelBuilder.Entity<Set>().HasData(
            new Set { Id = 1, Name = "Набор №1", Slug = "furniture-set-1", ImageUrl = "/src/assets/img/furniture/furniture-1.png" },
            new Set { Id = 2, Name = "Набор №2", Slug = "furniture-set-2", ImageUrl = "/src/assets/img/furniture/furniture-2.png" },
            new Set { Id = 3, Name = "Набор №3", Slug = "furniture-set-3", ImageUrl = "/src/assets/img/furniture/furniture-3.png" }
        );

        modelBuilder.Entity<SetItem>().HasData(
            new SetItem { Id = 1, SetId = 1, ProductId = 1, Quantity = 2 },
            new SetItem { Id = 2, SetId = 1, ProductId = 2, Quantity = 1 },
            new SetItem { Id = 3, SetId = 2, ProductId = 2, Quantity = 2 },
            new SetItem { Id = 4, SetId = 2, ProductId = 3, Quantity = 1 },
            new SetItem { Id = 5, SetId = 3, ProductId = 1, Quantity = 1 }
        );
        
        modelBuilder.Entity<NewArrival>().HasData(
            new NewArrival { Id = 1, ImageUrl = "/img/new_options_products/set-1.png",
                Text = "Sofa" },
            new NewArrival { Id = 2, ImageUrl = "/img/new_options_products/set-2.png",
                Text = "Decoration" },
            new NewArrival { Id = 3, ImageUrl = "/img/new_options_products/set-3.png",
                Text = "Pillow" }
        );
        
        modelBuilder.Entity<User>().HasData(
    new User {
        Id = 1,
        IsAdmin = true,
        FirstName = "Admin",
        LastName = "User",
        BirthDate = new DateTime(1990, 1, 1),
        Country = "UK",
        Address = "1 Admin Road",
        Phone = "+441234567890",
        Email = "admin@ikea.com",
        PasswordHash = Convert.FromBase64String("Lf7F8TSwnrGhxAPuxxXYdpDC9VSkSzrCJAkZbAZj5CGLm8pxm7r6QZtDZjLPJ1TbTgfck0+PeBLWYT8p9Ho3jw=="),
        PasswordSalt = Convert.FromBase64String("JG+zDJ2U0Dtvh3EmzRWeTIe2zgjTmc9/NylkpAZIoRQOHxdHfsqNjQ==")
    },
    new User {
        Id = 2,
        IsAdmin = false,
        FirstName = "Bob",
        LastName = "Brown",
        BirthDate = new DateTime(1992, 3, 4),
        Country = "USA",
        Address = "123 Maple Street",
        Phone = "+12125550000",
        Email = "bob@ikea.com",
        PasswordHash = Convert.FromBase64String("c1GAK2fs5heh9FVpJMjMkFxN5MiHG4bWBZDRbOg7tYFJk4ipft4vA8PpXL0Y9ZB5ipj+qUnMdYY0eUO98Vne9g=="),
        PasswordSalt = Convert.FromBase64String("0c1kQFuw1W6jKaJ6CQfKfVf1In9BRoQsDQTCrMkF7iUr9tvY0PrZFA==")
    },
    new User {
        Id = 3,
        IsAdmin = false,
        FirstName = "Charlie",
        LastName = "Clark",
        BirthDate = new DateTime(1991, 5, 10),
        Country = "Canada",
        Address = "45 Maple Avenue",
        Phone = "+14165551234",
        Email = "charlie@ikea.com",
        PasswordHash = Convert.FromBase64String("6HdYYjS8bN9Nm6rVG+KHppwDztzEPVWSHzYMZDxdtXQvhkbj2ZLaZMKfRrNAlVEOhL1kGyUE6+MojO4hxCEWrA=="),
        PasswordSalt = Convert.FromBase64String("5EyLiyKr+ySR+nqcd5zMGcNoO7DHrOmYZgyDNKWBamZOKs+UrP7P0Q==")
    },
    new User {
        Id = 4,
        IsAdmin = false,
        FirstName = "Diana",
        LastName = "Davis",
        BirthDate = new DateTime(1993, 9, 15),
        Country = "Germany",
        Address = "789 Berlin Str",
        Phone = "+4915112345678",
        Email = "diana@ikea.com",
        PasswordHash = Convert.FromBase64String("c9h0c8C5ZnEOxZJHOnrDqjz3sA6fC2nY04UCIc2hdrr2nGnDMuTVvNInM+djRJPibfBLpJJ0UzndeqkBdTO+zw=="),
        PasswordSalt = Convert.FromBase64String("UMiIGvH2uyzBk0dk8UsHKoz0AJIXuy93BTTJjZFYpy+ULoB6FtFgPw==")
    },
    new User {
        Id = 5,
        IsAdmin = false,
        FirstName = "Evan",
        LastName = "Evans",
        BirthDate = new DateTime(1995, 12, 20),
        Country = "France",
        Address = "22 Rue de Lyon",
        Phone = "+33142000000",
        Email = "evan@ikea.com",
        PasswordHash = Convert.FromBase64String("93gPQJ5vdJh0MoUIITjOtOIFuj2/VtUb8sbYZB5aFVvBzN6wWaHJkjbVz7mU3tdMjboYoRmnX+jC4Knd0ek6MA=="),
        PasswordSalt = Convert.FromBase64String("fXmn3ftk8xPBI/BVsnMQpgPlRwISl0gDhYBtXmbUoI0pCzETD1ZrsA==")
    }
    
    
);

    }
}
