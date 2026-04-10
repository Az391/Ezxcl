using System.Collections.Generic;

namespace Ezcel.Providers.Abstractions
{
    public interface IProviderFactory
    {
        IChatClient CreateClient(ProviderConfig config);
        bool Supports(ProviderType type);
        ProviderMetadata GetMetadata();
    }

    public class ProviderConfig
    {
        public string Name { get; set; }
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string ModelId { get; set; }
        public string ApiVersion { get; set; }
        public ProviderType Type { get; set; }
        public Dictionary<string, object> ExtraParams { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, string> CustomHeaders { get; set; } = new Dictionary<string, string>();
    }

    public enum ProviderType
    {
        OpenAI,
        Anthropic,
        GoogleGemini,
        DeepSeek,
        Ollama,
        AzureOpenAI,
        AWSBedrock,
        Mistral,
        OpenRouter,
        Custom
    }

    public class ProviderMetadata
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProviderType Type { get; set; }
        public bool SupportsCustomBaseUrl { get; set; }
        public bool SupportsCustomModel { get; set; }
    }
}