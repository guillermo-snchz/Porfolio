using Microsoft.Extensions.Caching.Memory;

namespace Porfolio.Web.Services.MemoryCache;

public class ApiMemoryCacheService : IApiCacheService
{
    private readonly IMemoryCache _cache;

    public ApiMemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T? Get<T>(string key)
    {
        return _cache.TryGetValue(key, out T? value) ? value : default;
    }

    public void Set<T>(string key, T data, TimeSpan? expiration = null)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromMinutes(5)
        };
        _cache.Set(key, data, options);
    }
}
