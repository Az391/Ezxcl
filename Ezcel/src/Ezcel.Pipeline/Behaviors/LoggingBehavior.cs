using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Pipeline.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("处理请求: {RequestType}", typeof(TRequest).Name);
            
            try
            {
                var response = await next();
                _logger.LogInformation("请求处理成功: {RequestType}", typeof(TRequest).Name);
                return response;
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "请求处理失败: {RequestType}", typeof(TRequest).Name);
                throw;
            }
        }
    }
}