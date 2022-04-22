using Microsoft.EntityFrameworkCore;
using shopApi.Models;

namespace shopApi.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }
    }

    public interface IDataContext
    {
        DbSet<Product> Products { get; set; }
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<int> SaveChangesAsync() => SaveChangesAsync(CancellationToken.None);
    }
}