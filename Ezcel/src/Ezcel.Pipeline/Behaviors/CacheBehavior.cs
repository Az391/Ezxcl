using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Pipeline.Behaviors
{
    public class CacheBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IMemoryCache _cache;

        public CacheBehavior(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var cacheKey = GetCacheKey(request);

            if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
            {
                return cachedResponse;
            }

            var response = await next();

            // 缓存结果，设置过期时间为1小时
            _cache.Set(cacheKey, response, new System.TimeSpan(1, 0, 0));

            return response;
        }

        private string GetCacheKey(TRequest request)
        {
            // 简单实现：使用请求的类型和内容作为缓存键
            return $"{typeof(TRequest).Name}:{request.GetHashCode()}";
        }
    }
}