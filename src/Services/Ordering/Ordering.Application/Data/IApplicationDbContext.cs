using Microsoft.EntityFrameworkCore;

namespace Ordering.Application.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Customer> Customers { get; }
        DbSet<Order> Orders { get; }
        DbSet<Product> Products { get; }
        DbSet<OrderItem> OrderItems { get; }

        //
        // Summary:
        //     Default Implementation for SaveChangesAsync method
        //
        // Parameters:
        //  cancellationToken:
        //      The cancellation token    
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
