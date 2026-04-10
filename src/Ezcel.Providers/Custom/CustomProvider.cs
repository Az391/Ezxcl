using Ezcel.Providers.Abstractions;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Providers.Custom
{
    public class CustomProvider : IProviderFactory
    {
        public IChatClient CreateClient(ProviderConfig config)
        {
            return new CustomChatClient(config);
        }

        public bool Supports(ProviderType type)
        {
            return type == ProviderType.Custom;
        }

        public ProviderMetadata GetMetadata()
        {
            return new ProviderMetadata
            {
                Name = "Custom",
                Description = "自定义模型提供者",
                Type = ProviderType.Custom,
                SupportsCustomBaseUrl = true,
                SupportsCustomModel = true
            };
        }
    }

    internal class CustomChatClient : IChatClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _model;
        private readonly string _baseUrl;

        public CustomChatClient(ProviderConfig config)
        {
            _httpClient = new HttpClient();
            _baseUrl = config.BaseUrl;
            _model = config.ModelId;

            // 设置API Key
            if (!string.IsNullOrEmpty(config.ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", config.ApiKey);
            }

            // 添加自定义 headers
            foreach (var header in config.CustomHeaders)
            {
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
            }
        }

        public async Task<ChatResponse> GetResponseAsync(
            IList<ChatMessage> messages,
            ChatOptions options,
            CancellationToken cancellationToken = default
        )
        {
            var requestBody = new Dictionary<string, object>
            {
                { "model", options.Model ?? _model },
                { "messages", messages },
                { "temperature", options.Temperature },
                { "max_tokens", options.MaxTokens }
            };

            // 添加额外参数
            foreach (var param in options.ExtraParams)
            {
                requestBody[param.Key] = param.Value;
            }

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/chat/completions", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<CustomResponse>(responseContent);

            return new ChatResponse
            {
                Content = result.choices[0].message.content,
                PromptTokens = result.usage.prompt_tokens,
                CompletionTokens = result.usage.completion_tokens,
                TotalTokens = result.usage.total_tokens
            };
        }

        private class CustomResponse
        {
            public List<Choice> choices { get; set; }
            public Usage usage { get; set; }
        }

        private class Choice
        {
            public Message message { get; set; }
        }

        private class Message
        {
            public string content { get; set; }
        }

        private class Usage
        {
            public int prompt_tokens { get; set; }
            public int completion_tokens { get; set; }
            public int total_tokens { get; set; }
        }
    }
}