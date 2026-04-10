using System.Text.Json;
using System.Threading.Tasks;

namespace Ezcel.Tools.Abstractions
{
    public interface IAITool
    {
        string Name { get; }
        string Description { get; }
        JsonSchema ParametersSchema { get; }
        Task<string> ExecuteAsync(JsonElement args);
    }

    public class JsonSchema
    {
        public string Type { get; set; } = "object";
        public JsonElement Properties { get; set; }
        public JsonElement Required { get; set; }
    }
}