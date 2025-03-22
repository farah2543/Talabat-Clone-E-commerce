using Domain.Entities;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync();

        IGenericRepository<TEntity, TKey> GenericRepository<TEntity, TKey>() where TEntity :BaseEntity<TKey>;


    }
}
