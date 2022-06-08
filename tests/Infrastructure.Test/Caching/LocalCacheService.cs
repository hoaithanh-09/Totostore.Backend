using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Test.Caching;

public class LocalCacheService : CacheService<Totostore.Backend.Infrastructure.Caching.LocalCacheService>
{
    protected override Totostore.Backend.Infrastructure.Caching.LocalCacheService CreateCacheService() =>
        new(
            new MemoryCache(new MemoryCacheOptions()),
            NullLogger<Totostore.Backend.Infrastructure.Caching.LocalCacheService>.Instance);
}