
namespace Ordering.Domain.Models.Orders
{
    public sealed record OrderCreatedEvent(Order order) : IDomainEvent;
}