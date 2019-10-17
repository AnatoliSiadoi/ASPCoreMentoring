using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Repositories
{
    public class EntityFrameworkContext : DbContext
    {
        public EntityFrameworkContext(DbContextOptions<EntityFrameworkContext> options) : base(options)
        {
        }

        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<SupplierEntity> Suppliers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<CategoryEntity>().Property(m => m.Id).HasColumnName("CategoryID");
            builder.Entity<ProductEntity>().Property(m => m.Id).HasColumnName("ProductID");
            builder.Entity<SupplierEntity>().Property(m => m.Id).HasColumnName("SupplierID");
        }
    }
}
