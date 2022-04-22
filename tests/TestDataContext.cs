using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using shopApi.Data;
using shopApi.Models;

public class TestDataContext : IDataContext
{
    public TestDataContext(DbSet<Product> dbSet)
    {
        this.Products = dbSet;
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual int SaveChanges()
    {
        return 0;
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(0);
    }
}