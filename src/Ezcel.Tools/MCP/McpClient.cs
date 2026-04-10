using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ezcel.Tools.MCP
{
    public class McpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public McpClient(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl;
        }

        public async Task<string> ExecuteAsync(string toolName, JsonElement args)
        {
            var requestBody = new
            {
                tool = toolName,
                args = args
            };

            var content = new StringContent(
                JsonSerializer.Serialize(requestBody),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{_baseUrl}/execute", content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}