using Ezcel.Providers.Abstractions;
using System.Collections.Generic;

namespace Ezcel.Configuration.Models
{
    public class AppConfig
    {
        public GlobalSettings GlobalSettings { get; set; } = new GlobalSettings();
        public Dictionary<string, ProviderConfig> Providers { get; set; } = new Dictionary<string, ProviderConfig>();
        public ResilienceSettings Resilience { get; set; } = new ResilienceSettings();
    }

    public class GlobalSettings
    {
        public string DefaultProvider { get; set; } = "openai";
        public string DefaultModel { get; set; } = "gpt-4o-mini";
        public double DefaultTemperature { get; set; } = 0.0;
        public int MaxOutputTokens { get; set; } = 8192;
        public bool EnableCache { get; set; } = true;
        public bool EnableFileLogging { get; set; } = true;
    }

    public class ResilienceSettings
    {
        public int TokenBucketRate { get; set; } = 2;
        public int MaxConcurrentRequests { get; set; } = 4;
        public int MaxRetries { get; set; } = 5;
        public int HttpTimeoutSeconds { get; set; } = 600;
    }
}