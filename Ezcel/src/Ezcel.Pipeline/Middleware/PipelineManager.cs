using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ezcel.Pipeline.Middleware
{
    public static class PipelineManager
    {
        public static IServiceCollection AddPipelineBehaviors(this IServiceCollection services)
        {
            // 注册管道行为
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.CacheBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.RateLimitBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.RetryBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.UsageTrackingBehavior<,>));

            return services;
        }
    }
}