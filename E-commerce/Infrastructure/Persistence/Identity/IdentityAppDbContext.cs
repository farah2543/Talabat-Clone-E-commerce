using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace persistence.Identity
{
    public class IdentityAppDbContext : IdentityDbContext
    {
        public IdentityAppDbContext(DbContextOptions<IdentityAppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
        }


    }
}
