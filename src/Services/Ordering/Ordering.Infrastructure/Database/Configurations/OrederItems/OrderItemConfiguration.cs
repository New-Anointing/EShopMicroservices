using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Models.OrderItems;
using Ordering.Domain.Models.Products;
using Ordering.Domain.ValueObjects;

namespace Ordering.Infrastructure.Database.Configurations.OrederItems
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(orderItemId => orderItemId.Id);

            builder
                .Property(orderItemId => orderItemId.Id)
                .HasConversion(orderItemId => orderItemId.Value,
                databaseId => OrderItemId.Of(databaseId));

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(orderItem => orderItem.ProductId);

            builder.Property(orderItemId => orderItemId.Id).IsRequired();

            builder.Property(orderItem => orderItem.Price).IsRequired();

        }
    }
}
