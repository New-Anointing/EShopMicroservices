
namespace Basket.API.Services
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken token = default)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, token);
            return basket is null ? throw new BasketNotFoundException(userName) :  basket;
        }

        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken token = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(token);
            return basket;
        }
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken token = default)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(token);
            return true;
        }

    }
}
