﻿using Domain.Entities;

namespace Domain.Contracts
{
    public interface IGenericRepository <TEntity,TKey> where TEntity : BaseEntity<TKey>
    {

        Task<TEntity?> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking);

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);




    }
}
