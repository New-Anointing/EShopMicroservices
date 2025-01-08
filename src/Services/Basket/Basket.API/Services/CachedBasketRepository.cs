using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Services
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasketAsync(string userName, CancellationToken token = default)
        {
            var cachedBasket = await cache.GetStringAsync(userName, token);
            if (!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(json: cachedBasket)!;
            }
            var basket = await basketRepository.GetBasketAsync(userName, token);
            await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            }, token);
            return basket;
        }
        public async Task<ShoppingCart> StoreBasketAsync(ShoppingCart basket, CancellationToken token = default)
        {
            var storedBasket = await basketRepository.StoreBasketAsync(basket, token);
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(storedBasket), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            }, token);
            return storedBasket;
        }
        public async Task<bool> DeleteBasketAsync(string userName, CancellationToken token = default)
        {
            await cache.RemoveAsync(userName, token);
            return await basketRepository.DeleteBasketAsync(userName, token);
        }
    }
}
