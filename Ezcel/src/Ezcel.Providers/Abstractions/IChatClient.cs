using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ezcel.Providers.Abstractions
{
    public interface IChatClient
    {
        Task<ChatResponse> GetResponseAsync(
            IList<ChatMessage> messages,
            ChatOptions options,
            CancellationToken cancellationToken = default
        );
    }

    public class ChatMessage
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }

    public class ChatOptions
    {
        public string Model { get; set; }
        public double Temperature { get; set; }
        public int MaxTokens { get; set; }
        public Dictionary<string, object> ExtraParams { get; set; } = new Dictionary<string, object>();
    }

    public class ChatResponse
    {
        public string Content { get; set; }
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }
}