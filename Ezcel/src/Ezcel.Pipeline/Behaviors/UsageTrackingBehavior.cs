using MediatR;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Pipeline.Behaviors
{
    public class UsageTrackingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            try
            {
                var response = await next();

                stopwatch.Stop();
                
                // 记录使用情况
                TrackUsage(response, stopwatch.Elapsed);

                return response;
            }
            catch (System.Exception ex)
            {
                stopwatch.Stop();
                // 记录错误情况下的使用情况
                System.Console.WriteLine($"请求失败，耗时: {stopwatch.Elapsed.TotalSeconds}秒");
                throw;
            }
        }

        private void TrackUsage(TResponse response, TimeSpan elapsed)
        {
            // 简单实现：打印使用情况
            // 实际项目中可以存储到数据库或文件
            System.Console.WriteLine($"请求耗时: {elapsed.TotalSeconds}秒");
            
            // 如果响应包含Token使用信息，可以在这里提取
            // 例如：if (response is ChatResponse chatResponse) { ... }
        }
    }
}