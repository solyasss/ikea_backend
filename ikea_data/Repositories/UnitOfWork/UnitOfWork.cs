using ikea_data.Data;
using ikea_data.Repositories.Interfaces;

namespace ikea_data.Repositories.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IkeaDbContext _db;

        public UnitOfWork(
            IkeaDbContext                     db,
            ICategoryRepository               categories,
            IProductRepository                products,
            IUserRepository                   users,
            INewArrivalRepository             arrivals,
            ISetRepository                    sets,
            IProductImageRepository           images,
            IProductCommentRepository         comments,
            IProductCharacteristicRepository  chars,
            IUserCardRepository               cards,
            IWishlistRepository               wishlists,
            ICartRepository     carts,
            IOrderRepository orders)

        {
            _db             = db;
            Categories      = categories;
            Products        = products;
            Users           = users;
            NewArrivals     = arrivals;
            Sets            = sets;
            Images          = images;
            Comments        = comments;
            Characteristics = chars;
            UserCards       = cards;
            Wishlists       = wishlists;  
            Carts     = carts;
            Orders = orders;

        }

        public ICategoryRepository              Categories       { get; }
        public IProductRepository               Products         { get; }
        public IUserRepository                  Users            { get; }
        public INewArrivalRepository            NewArrivals      { get; }
        public ISetRepository                   Sets             { get; }
        public IProductImageRepository          Images           { get; }
        public IProductCommentRepository        Comments         { get; }
        public IProductCharacteristicRepository Characteristics  { get; }
        public IUserCardRepository              UserCards        { get; }
        public IWishlistRepository              Wishlists        { get; }   
        public ICartRepository  Carts { get; }
        public IOrderRepository  Orders { get; }


        public async Task<int> SaveAsync() => await _db.SaveChangesAsync();
        public void Dispose()               => _db.Dispose();
    }
}
