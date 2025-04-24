namespace Porfolio.Web.Services.MemoryCache;

public interface IApiCacheService
{
    T? Get<T>(string key);
    void Set<T>(string key, T data, TimeSpan? expiration = null);
}
