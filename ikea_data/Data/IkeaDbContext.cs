using System.Security.Cryptography;
using ikea_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ikea_data.Data
{
    public class IkeaDbContext : DbContext
    {
        public IkeaDbContext(DbContextOptions<IkeaDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ProductCharacteristic> ProductCharacteristics => Set<ProductCharacteristic>();
        public DbSet<ProductImage> ProductImages => Set<ProductImage>();
        public DbSet<ProductComment> ProductComments => Set<ProductComment>();
        public DbSet<Set> Sets => Set<Set>();
        public DbSet<SetItem> SetItems => Set<SetItem>();
        public DbSet<User> Users => Set<User>();
        public DbSet<NewArrival> NewArrivals => Set<NewArrival>();
        
        public DbSet<UserCard> UserCards => Set<UserCard>();
        
        public DbSet<Wishlist> Wishlists => Set<Wishlist>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<Order> Orders => Set<Order>();


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            
            // ----- конфигурация decimal -----
            modelBuilder.Entity<Product>(e =>
            {
                e.Property(p => p.Price)   .HasPrecision(10, 2); 
                e.Property(p => p.Weight)  .HasPrecision(8, 2);  
                e.Property(p => p.Rating)  .HasPrecision(3, 2); 
            });

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

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductCharacteristic>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Characteristics)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductComment>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.Comments)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductComment>()
                .HasOne(pc => pc.User)
                .WithMany()
                .HasForeignKey(pc => pc.UserId)
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
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Description)
                .HasMaxLength(1000);  
            
            modelBuilder.Entity<UserCard>(e =>
            {
                e.Property(c => c.CardNumber).HasMaxLength(16);
                e.Property(c => c.CardType).HasMaxLength(32);

                e.HasOne(c => c.User)
                    .WithMany(u => u.Cards)
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Wishlist>(e =>
            {
                e.HasIndex(w => new { w.UserId, w.ProductId }).IsUnique();  
                e.HasOne(w => w.User)
                    .WithMany()
                    .HasForeignKey(w => w.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(w => w.Product)
                    .WithMany()
                    .HasForeignKey(w => w.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Order>(e =>
            {
                e.Property(o => o.TotalSum).HasPrecision(10, 2);
                e.HasIndex(o => o.OrderNumber).IsUnique();

                e.HasOne(o => o.User)
                    .WithMany()
                    .HasForeignKey(o => o.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(o => o.Product)
                    .WithMany()
                    .HasForeignKey(o => o.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasData(
                    new Order
                    {
                        Id = 1, OrderNumber = "Order-1",
                        UserId = 2, ProductId = 1,
                        OrderDate = new DateTime(2025, 5, 20),
                        ReceiveDate = new DateTime(2025, 5, 25),
                        IsCash = false, TotalSum = 59.99m
                    },
                    new Order
                    {
                        Id = 2, OrderNumber = "Order-2",
                        UserId = 3, ProductId = 2,
                        OrderDate = new DateTime(2025, 5, 21),
                        ReceiveDate = new DateTime(2025, 5, 26),
                        IsCash = true, TotalSum = 19.99m
                    }
                );
            });



            // ---------- сиды ----------
            modelBuilder.Entity<UserCard>().HasData(
                new UserCard
                {
                    Id = 1,
                    UserId = 2, 
                    CardNumber = "1111222233334444",
                    ValidDay = 1,
                    ValidYear = 2026,
                    CardType = "visa",
                    CvvHash = SHA256("123")
                },
                new UserCard
                {
                    Id = 2,
                    UserId = 3, 
                    CardNumber = "5555666677778888",
                    ValidDay = 15,
                    ValidYear = 2025,
                    CardType = "mastercard",
                    CvvHash = SHA256("456")
                },
                new UserCard
                {
                    Id = 3,
                    UserId = 4,
                    CardNumber = "9999000011112222",
                    ValidDay = 30,
                    ValidYear = 2027,
                    CardType = "visa",
                    CvvHash = SHA256("789")
                }
            );
            
            modelBuilder.Entity<Wishlist>(e =>
            {
                e.HasIndex(w => new { w.UserId, w.ProductId }).IsUnique();  
                e.HasOne(w => w.User)
                    .WithMany()
                    .HasForeignKey(w => w.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasOne(w => w.Product)
                    .WithMany()
                    .HasForeignKey(w => w.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);
    
                e.HasData(
                    new Wishlist { Id = 1, UserId = 2, ProductId = 1 },
                    new Wishlist { Id = 2, UserId = 2, ProductId = 2 },
                    new Wishlist { Id = 3, UserId = 3, ProductId = 1 }
                );
            });

            modelBuilder.Entity<Cart>(e =>
            {
                e.Property(c => c.TotalSum).HasPrecision(10, 2);        
                e.HasIndex(c => new { c.UserId, c.ProductId }).IsUnique();
                e.HasOne(c => c.User)
                    .WithMany()
                    .HasForeignKey(c => c.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
                e.HasOne(c => c.Product)
                    .WithMany()
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Cascade);

                e.HasData(
                    new Cart { Id = 1, UserId = 2, ProductId = 1, Quantity = 1, IsCash = true,  TotalSum = 59.99m },
                    new Cart { Id = 2, UserId = 3, ProductId = 2, Quantity = 2, IsCash = false, TotalSum = 39.98m }
                );
            });

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, ParentId = null, Title = "Гостиная", Slug = "living-room" },
                new Category { Id = 2, ParentId = null, Title = "Спальня", Slug = "bedroom" },
                new Category { Id = 3, ParentId = null, Title = "Кухня и столовая", Slug = "kitchen-and-dining" },
                new Category { Id = 4, ParentId = null, Title = "Ванная", Slug = "bathroom" },
                new Category { Id = 5, ParentId = null, Title = "Офис", Slug = "office" },
                new Category { Id = 6, ParentId = 1, Title = "Диваны", Slug = "sofas" },
                new Category { Id = 7, ParentId = 1, Title = "Кресла", Slug = "armchairs" },
                new Category { Id = 8, ParentId = 1, Title = "Журнальные столики", Slug = "coffee-tables" },
                new Category { Id = 9, ParentId = 1, Title = "Шкафы и полки", Slug = "wardrobes-and-shelves" },
                new Category { Id = 10, ParentId = 1, Title = "Стеллажи для ТВ", Slug = "tv-stands" },
                new Category { Id = 11, ParentId = 1, Title = "Ковры", Slug = "carpets" },
                new Category { Id = 12, ParentId = 1, Title = "Освещение", Slug = "lighting" },
                new Category { Id = 13, ParentId = 1, Title = "Декор", Slug = "decor" },
                new Category { Id = 14, ParentId = 1, Title = "Системы хранения", Slug = "storage" },
                new Category { Id = 15, ParentId = 2, Title = "Кровати", Slug = "beds" },
                new Category { Id = 16, ParentId = 2, Title = "Матрасы", Slug = "mattresses" },
                new Category { Id = 17, ParentId = 2, Title = "Шкафы", Slug = "closets" },
                new Category { Id = 18, ParentId = 3, Title = "Обеденные столы", Slug = "dining-tables" },
                new Category { Id = 19, ParentId = 3, Title = "Стулья", Slug = "chairs" },
                new Category { Id = 20, ParentId = 3, Title = "Барные стулья", Slug = "bar-stools" },
                new Category { Id = 21, ParentId = 3, Title = "Посуда", Slug = "dishes" },
                new Category { Id = 22, ParentId = 3, Title = "Сервировка стола", Slug = "table-setting" },
                new Category { Id = 23, ParentId = 3, Title = "Столовые приборы", Slug = "cutlery" },
                new Category { Id = 24, ParentId = 4, Title = "Полки и шкафчики", Slug = "shelves-and-cabinets" },
                new Category { Id = 25, ParentId = 4, Title = "Полотенца", Slug = "towels" },
                new Category { Id = 26, ParentId = 4, Title = "Шторки для душа", Slug = "shower-curtains" },
                new Category { Id = 27, ParentId = 5, Title = "Письменные столы", Slug = "desks" },
                new Category { Id = 28, ParentId = 5, Title = "Офисные кресла", Slug = "office-chairs" },
                new Category { Id = 29, ParentId = 5, Title = "Книжные шкафы", Slug = "bookshelves" },
                new Category { Id = 30, ParentId = 5, Title = "Настольные лампы", Slug = "desk-lamps" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Article = "CHAIR-001",
                    CategoryId = 19,
                    Name = "Comfort Chair",
                    Price = 59.99m,
                    MainImage   = "/src/assets/img/product_details/product-1.png",
                    Description = "Удобное коричневое кресло для гостиной",  
                    Color = "Brown",
                    Dimensions = "60x60x90",
                    Weight = 7.5m,
                    Type = "Armchair",
                    CountryOfOrigin = "Poland",
                    PackageContents = "1x Chair, Instructions",
                    Warranty = "2 years",
                    Materials = "Wood, Textile",
                    Rating = 4.5m
                },
                new Product
                {
                    Id = 2,
                    Article = "LAMP-002",
                    CategoryId = 12,
                    Name = "Minimalist Lamp",
                    Price = 19.99m,
                    MainImage   = "/src/assets/img/product_details/product-2.png",
                    Description = "Минималистичная настольная лампа — белый пластик",
                    Color = "White",
                    Dimensions = "15x15x45",
                    Weight = 1.2m,
                    Type = "Desk Lamp",
                    CountryOfOrigin = "Germany",
                    PackageContents = "1x Lamp, Bulb included",
                    Warranty = "1 year",
                    Materials = "Plastic, Metal",
                    Rating = 4.0m
                },
                new Product
                {
                    Id = 3,
                    Article = "LAMP-003",
                    CategoryId = 12,
                    Name = "Vintage Lamp",
                    Price = 19.99m,
                    MainImage   = "/src/assets/img/product_details/product-3.png",
                    Description = "Винтажный напольный светильник — матовый чёрный",
                    Color = "Black",
                    Dimensions = "18x18x50",
                    Weight = 1.5m,
                    Type = "Floor Lamp",
                    CountryOfOrigin = "Italy",
                    PackageContents = "1x Lamp",
                    Warranty = "1 year",
                    Materials = "Metal, Glass",
                    Rating = 3.8m
                }
            );


            modelBuilder.Entity<ProductImage>().HasData(
                new ProductImage
                    { Id = 1, ProductId = 1, ImageUrl = "/src/assets/img/products/product-1.png", SortOrder = 0 },
                new ProductImage
                    { Id = 2, ProductId = 2, ImageUrl = "/src/assets/img/products/product-2.png", SortOrder = 0 },
                new ProductImage
                    { Id = 3, ProductId = 3, ImageUrl = "/src/assets/img/products/product-3.png", SortOrder = 0 }
            );

            modelBuilder.Entity<ProductComment>().HasData(
                new ProductComment
                {
                    Id = 1,
                    ProductId = 1,
                    UserId = 2, 
                    CommentText = "Nice chair",
                    Rating = 5
                },
                new ProductComment
                {
                    Id = 2,
                    ProductId = 2,
                    UserId = 3, 
                    CommentText = "Bright lamp",
                    Rating = 4
                }
            );

           

            
            modelBuilder.Entity<ProductCharacteristic>().HasData(
                new ProductCharacteristic { Id = 1, ProductId = 1, Name = "Material", Value = "Wood" },
                new ProductCharacteristic { Id = 2, ProductId = 1, Name = "Color", Value = "Brown" },
                new ProductCharacteristic { Id = 3, ProductId = 2, Name = "Color", Value = "White" }
            );

            modelBuilder.Entity<Set>().HasData(
                new Set
                {
                    Id = 1, Name = "Набор №1", Slug = "furniture-set-1",
                    ImageUrl = "/src/assets/img/furniture/furniture-1.png"
                },
                new Set
                {
                    Id = 2, Name = "Набор №2", Slug = "furniture-set-2",
                    ImageUrl = "/src/assets/img/furniture/furniture-2.png"
                },
                new Set
                {
                    Id = 3, Name = "Набор №3", Slug = "furniture-set-3",
                    ImageUrl = "/src/assets/img/furniture/furniture-3.png"
                }
            );

            modelBuilder.Entity<SetItem>().HasData(
                new SetItem { Id = 1, SetId = 1, ProductId = 1, Quantity = 2 },
                new SetItem { Id = 2, SetId = 1, ProductId = 2, Quantity = 1 },
                new SetItem { Id = 3, SetId = 2, ProductId = 2, Quantity = 2 },
                new SetItem { Id = 4, SetId = 2, ProductId = 3, Quantity = 1 },
                new SetItem { Id = 5, SetId = 3, ProductId = 1, Quantity = 1 }
            );

            modelBuilder.Entity<NewArrival>().HasData(
                new NewArrival
                {
                    Id = 1, ImageUrl = "/img/new_options_products/set-1.png",
                    Text = "Sofa"
                },
                new NewArrival
                {
                    Id = 2, ImageUrl = "/img/new_options_products/set-2.png",
                    Text = "Decoration"
                },
                new NewArrival
                {
                    Id = 3, ImageUrl = "/img/new_options_products/set-3.png",
                    Text = "Pillow"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    IsAdmin = true,
                    FirstName = "Admin",
                    LastName = "User",
                    BirthDate = new DateTime(1990, 1, 1),
                    Country = "UK",
                    Address = "1 Admin Road",
                    Phone = "+441234567890",
                    Email = "admin@ikea.com",
                    PasswordHash =
                        Convert.FromBase64String(
                            "Lf7F8TSwnrGhxAPuxxXYdpDC9VSkSzrCJAkZbAZj5CGLm8pxm7r6QZtDZjLPJ1TbTgfck0+PeBLWYT8p9Ho3jw=="),
                    PasswordSalt = Convert.FromBase64String("JG+zDJ2U0Dtvh3EmzRWeTIe2zgjTmc9/NylkpAZIoRQOHxdHfsqNjQ==")
                },
                new User
                {
                    Id = 2,
                    IsAdmin = false,
                    FirstName = "Bob",
                    LastName = "Brown",
                    BirthDate = new DateTime(1992, 3, 4),
                    Country = "USA",
                    Address = "123 Maple Street",
                    Phone = "+12125550000",
                    Email = "bob@ikea.com",
                    PasswordHash =
                        Convert.FromBase64String(
                            "c1GAK2fs5heh9FVpJMjMkFxN5MiHG4bWBZDRbOg7tYFJk4ipft4vA8PpXL0Y9ZB5ipj+qUnMdYY0eUO98Vne9g=="),
                    PasswordSalt = Convert.FromBase64String("0c1kQFuw1W6jKaJ6CQfKfVf1In9BRoQsDQTCrMkF7iUr9tvY0PrZFA==")
                },
                new User
                {
                    Id = 3,
                    IsAdmin = false,
                    FirstName = "Charlie",
                    LastName = "Clark",
                    BirthDate = new DateTime(1991, 5, 10),
                    Country = "Canada",
                    Address = "45 Maple Avenue",
                    Phone = "+14165551234",
                    Email = "charlie@ikea.com",
                    PasswordHash =
                        Convert.FromBase64String(
                            "6HdYYjS8bN9Nm6rVG+KHppwDztzEPVWSHzYMZDxdtXQvhkbj2ZLaZMKfRrNAlVEOhL1kGyUE6+MojO4hxCEWrA=="),
                    PasswordSalt = Convert.FromBase64String("5EyLiyKr+ySR+nqcd5zMGcNoO7DHrOmYZgyDNKWBamZOKs+UrP7P0Q==")
                },
                new User
                {
                    Id = 4,
                    IsAdmin = false,
                    FirstName = "Diana",
                    LastName = "Davis",
                    BirthDate = new DateTime(1993, 9, 15),
                    Country = "Germany",
                    Address = "789 Berlin Str",
                    Phone = "+4915112345678",
                    Email = "diana@ikea.com",
                    PasswordHash =
                        Convert.FromBase64String(
                            "c9h0c8C5ZnEOxZJHOnrDqjz3sA6fC2nY04UCIc2hdrr2nGnDMuTVvNInM+djRJPibfBLpJJ0UzndeqkBdTO+zw=="),
                    PasswordSalt = Convert.FromBase64String("UMiIGvH2uyzBk0dk8UsHKoz0AJIXuy93BTTJjZFYpy+ULoB6FtFgPw==")
                },
                new User
                {
                    Id = 5,
                    IsAdmin = false,
                    FirstName = "Evan",
                    LastName = "Evans",
                    BirthDate = new DateTime(1995, 12, 20),
                    Country = "France",
                    Address = "22 Rue de Lyon",
                    Phone = "+33142000000",
                    Email = "evan@ikea.com",
                    PasswordHash =
                        Convert.FromBase64String(
                            "93gPQJ5vdJh0MoUIITjOtOIFuj2/VtUb8sbYZB5aFVvBzN6wWaHJkjbVz7mU3tdMjboYoRmnX+jC4Knd0ek6MA=="),
                    PasswordSalt = Convert.FromBase64String("fXmn3ftk8xPBI/BVsnMQpgPlRwISl0gDhYBtXmbUoI0pCzETD1ZrsA==")
                }
            );
        }
        private static byte[] SHA256(string input)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            return sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        }
    }
    

    public class SampleContextFactory : IDesignTimeDbContextFactory<IkeaDbContext>
    {
        public IkeaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IkeaDbContext>();

            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
            return new IkeaDbContext(optionsBuilder.Options);
        }
    }
   

}