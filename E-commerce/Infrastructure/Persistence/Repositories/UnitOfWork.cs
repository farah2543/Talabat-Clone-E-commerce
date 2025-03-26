
namespace persistence.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        private ConcurrentDictionary<string, object> _repositories; //MultiThread

        public UnitOfWork(ApplicationDbContext dbContext) {

            _dbContext = dbContext;
            _repositories = new ();   
        }

        public IGenericRepository<TEntity, TKey> GenericRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            // return new GenericRepository<TEntity, TKey>(_dbContext);
            // Dictionary [collections]
            // KEY : Value
            // Repositories
            // Key : Value
            // Product : new GenericRepository<Product, int>

            // var typeName = typeof(TEntity).Name; // KEY
            // if(_repositories.ContainsKey(typeName))
            //      return (IGenericRepository<TEntity, TKey>) _repositories[typeName];
            // else
            // {
            //      var repo = new GenericRepository<TEntity, TKey>(_dbContext);
            //      _repositories.Add(typeName, repo);
            //      return repo;
            // }

            return (IGenericRepository<TEntity,TKey>)
                _repositories.GetOrAdd(typeof(TEntity).Name, (_) => new GenericRepository<TEntity, TKey>(_dbContext));

        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
        
    }
}
