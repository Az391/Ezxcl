using ExcelDna.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace Ezcel.AddIn
{
    public class AddIn : IExcelAddIn
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public void AutoOpen()
        {
            // 配置日志
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("ezcel.log")
                .CreateLogger();

            // 创建服务容器
            var services = new ServiceCollection();
            services.AddLogging(builder => builder.AddSerilog());
            
            // 注册其他服务
            ConfigureServices(services);

            ServiceProvider = services.BuildServiceProvider();

            ExcelIntegration.RegisterUnhandledExceptionHandler(ex =>
            {
                Log.Error(ex, "Unhandled exception");
                return "Error: " + ex.Message;
            });

            Log.Information("Ezcel AddIn loaded");
        }

        public void AutoClose()
        {
            Log.Information("Ezcel AddIn unloaded");
            Log.CloseAndFlush();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            // 这里将在后续实现中添加服务注册
        }
    }
}