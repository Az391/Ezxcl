using Ezcel.Tools.Abstractions;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ezcel.Tools.Native
{
    public class FileSearchTool : IAITool
    {
        public string Name => "file_search";
        public string Description => "使用glob模式搜索本地磁盘文件";

        public JsonSchema ParametersSchema => new JsonSchema
        {
            Type = "object",
            Properties = JsonDocument.Parse(@"{
                ""pattern"": {""type"": ""string"", ""description"": ""搜索模式，支持glob通配符""},
                ""directory"": {""type"": ""string"", ""description"": ""搜索目录，默认为当前目录""}
            }").RootElement,
            Required = JsonDocument.Parse("[\"pattern\"]").RootElement
        };

        public async Task<string> ExecuteAsync(JsonElement args)
        {
            string pattern = args.GetProperty("pattern").GetString();
            string directory = args.TryGetProperty("directory", out var dirElement) ? dirElement.GetString() : Directory.GetCurrentDirectory();

            if (!Directory.Exists(directory))
            {
                return "目录不存在";
            }

            var files = Directory.GetFiles(directory, pattern, SearchOption.AllDirectories);
            var result = string.Join("\n", files);

            return string.IsNullOrEmpty(result) ? "没有找到匹配的文件" : result;
        }
    }
}