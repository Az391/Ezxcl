using Ezcel.Providers.Abstractions;
using OpenAI;
using OpenAI.Chat;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Providers.BuiltIn
{
    public class OpenAIProvider : IProviderFactory
    {
        public IChatClient CreateClient(ProviderConfig config)
        {
            return new OpenAIChatClient(config);
        }

        public bool Supports(ProviderType type)
        {
            return type == ProviderType.OpenAI;
        }

        public ProviderMetadata GetMetadata()
        {
            return new ProviderMetadata
            {
                Name = "OpenAI",
                Description = "OpenAI 模型提供者",
                Type = ProviderType.OpenAI,
                SupportsCustomBaseUrl = true,
                SupportsCustomModel = true
            };
        }
    }

    internal class OpenAIChatClient : IChatClient
    {
        private readonly OpenAIClient _client;
        private readonly string _model;

        public OpenAIChatClient(ProviderConfig config)
        {
            var options = new OpenAIClientOptions
            {
                ApiKey = config.ApiKey
            };

            if (!string.IsNullOrEmpty(config.BaseUrl))
            {
                options.Endpoint = config.BaseUrl;
            }

            _client = new OpenAIClient(options);
            _model = config.ModelId ?? "gpt-4o-mini";
        }

        public async Task<ChatResponse> GetResponseAsync(
            IList<ChatMessage> messages,
            ChatOptions options,
            CancellationToken cancellationToken = default
        )
        {
            var chatMessages = new List<OpenAI.Chat.ChatMessage>();
            foreach (var msg in messages)
            {
                chatMessages.Add(new OpenAI.Chat.ChatMessage
                {
                    Role = msg.Role switch
                    {
                        "system" => OpenAI.Chat.ChatRole.System,
                        "assistant" => OpenAI.Chat.ChatRole.Assistant,
                        _ => OpenAI.Chat.ChatRole.User
                    },
                    Content = msg.Content
                });
            }

            var chatRequest = new ChatCompletionRequest
            {
                Model = options.Model ?? _model,
                Messages = chatMessages,
                Temperature = (float)options.Temperature,
                MaxTokens = options.MaxTokens
            };

            var response = await _client.ChatEndpoint.GetCompletionAsync(chatRequest, cancellationToken);

            return new ChatResponse
            {
                Content = response.Choices[0].Message.Content,
                PromptTokens = response.Usage.PromptTokens,
                CompletionTokens = response.Usage.CompletionTokens,
                TotalTokens = response.Usage.TotalTokens
            };
        }
    }
}