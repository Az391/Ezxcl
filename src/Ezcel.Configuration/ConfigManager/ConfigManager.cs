using Ezcel.Configuration.Models;
using Ezcel.Configuration.SecureStorage;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Ezcel.Configuration.ConfigManager
{
    public class ConfigManager
    {
        private readonly IConfiguration _configuration;
        private readonly SecureStorage _secureStorage;
        private AppConfig _appConfig;

        public ConfigManager()
        {
            _secureStorage = new SecureStorage();

            // 构建配置
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            _configuration = builder.Build();
            LoadConfig();

            // 监听配置变化
            ((IConfigurationRoot)_configuration).Reload();
        }

        public AppConfig GetConfig()
        {
            return _appConfig;
        }

        public void SaveConfig(AppConfig config)
        {
            // 加密API Key
            foreach (var provider in config.Providers)
            {
                if (!string.IsNullOrEmpty(provider.Value.ApiKey) && !provider.Value.ApiKey.StartsWith("[ENCRYPTED]"))
                {
                    provider.Value.ApiKey = "[ENCRYPTED]" + _secureStorage.Encrypt(provider.Value.ApiKey);
                }
            }

            _appConfig = config;
            // 实际项目中应该将配置写回文件
        }

        private void LoadConfig()
        {
            _appConfig = new AppConfig();
            _configuration.Bind(_appConfig);

            // 解密API Key
            foreach (var provider in _appConfig.Providers)
            {
                if (!string.IsNullOrEmpty(provider.Value.ApiKey) && provider.Value.ApiKey.StartsWith("[ENCRYPTED]"))
                {
                    var encryptedKey = provider.Value.ApiKey.Substring("[ENCRYPTED]".Length);
                    provider.Value.ApiKey = _secureStorage.Decrypt(encryptedKey);
                }
            }
        }
    }
}