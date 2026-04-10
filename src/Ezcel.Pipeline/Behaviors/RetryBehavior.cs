using MediatR;
using Polly;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Pipeline.Behaviors
{
    public class RetryBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            // 配置指数退避重试策略
            var retryPolicy = Policy
                .Handle<System.Exception>()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt - 1) * 1.5),
                    onRetry: (exception, timespan, attempt, context) =>
                    {
                        // 记录重试信息
                        System.Console.WriteLine($"重试 {attempt}/5: {exception.Message}");
                    }
                );

            return await retryPolicy.ExecuteAsync(async () =>
            {
                return await next();
            });
        }
    }
}