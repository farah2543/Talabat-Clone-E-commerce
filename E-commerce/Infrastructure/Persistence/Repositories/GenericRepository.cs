

namespace persistence.Repositories
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {

        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);


        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);


        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);

        public async Task<TEntity> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);



        public async Task<IEnumerable<TEntity>> GetAllAsync(bool asNoTracking) =>
            asNoTracking ?
                await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync() // True
                : await _dbContext.Set<TEntity>().ToListAsync(); // False






    }
}

