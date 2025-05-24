using System;
using System.Threading.Tasks;

namespace ikea_data.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository              Categories      { get; }
        IProductRepository               Products        { get; }
        IUserRepository                  Users           { get; }
        INewArrivalRepository            NewArrivals     { get; }
        ISetRepository                   Sets            { get; }
        IProductImageRepository          Images          { get; }
        IProductCommentRepository        Comments        { get; }
        IProductCharacteristicRepository Characteristics { get; }
        IUserCardRepository UserCards { get; }
        IWishlistRepository Wishlists { get; }

//ntcn 
        Task<int> SaveAsync();
    }
}