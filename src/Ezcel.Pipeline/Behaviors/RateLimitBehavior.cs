using MediatR;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Pipeline.Behaviors
{
    public class RateLimitBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(4); // 最大4个并发请求
        private static readonly ConcurrentDictionary<string, int> _tokenBucket = new ConcurrentDictionary<string, int>();
        private static readonly object _lock = new object();

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 并发控制
            await _semaphore.WaitAsync(cancellationToken);

            try
            {
                // 令牌桶限流（简单实现）
                await WaitForTokenAsync(cancellationToken);

                return await next();
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task WaitForTokenAsync(CancellationToken cancellationToken)
        {
            // 简单的令牌桶实现
            // 每秒钟生成2个令牌
            while (true)
            {
                lock (_lock)
                {
                    var key = System.DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                    if (!_tokenBucket.TryGetValue(key, out var tokens) || tokens < 2)
                    {
                        _tokenBucket[key] = tokens + 1;
                        return;
                    }
                }
                await Task.Delay(100, cancellationToken);
            }
        }
    }
}