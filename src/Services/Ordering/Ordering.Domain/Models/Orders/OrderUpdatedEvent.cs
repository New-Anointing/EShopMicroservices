
namespace Ordering.Domain.Models.Orders
{
    public sealed record OrderUpdatedEvent(Order order) : IDomainEvent;

}