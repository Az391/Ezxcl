using System.Collections.Generic;

namespace Ezcel.Prompt.SystemMessages
{
    public class SystemMessageManager
    {
        private readonly Dictionary<string, string> _systemMessages;

        public SystemMessageManager()
        {
            _systemMessages = new Dictionary<string, string>
            {
                { "default", "你是一个数据分析助手，擅长处理Excel中的数据。请简洁明了地回答问题，提供准确的分析结果。" },
                { "summarize", "请对给定的数据进行总结，提取关键信息和趋势。" },
                { "analyze", "请分析给定的数据，识别模式和异常。" },
                { "transform", "请按照要求转换数据格式。" },
                { "translate", "请将文本翻译成指定语言。" }
            };
        }

        public string GetSystemMessage(string type = "default")
        {
            if (_systemMessages.TryGetValue(type, out var message))
            {
                return message;
            }
            return _systemMessages["default"];
        }

        public void AddSystemMessage(string type, string message)
        {
            _systemMessages[type] = message;
        }

        public void RemoveSystemMessage(string type)
        {
            if (type != "default")
            {
                _systemMessages.Remove(type);
            }
        }

        public List<string> GetAvailableTypes()
        {
            return new List<string>(_systemMessages.Keys);
        }
    }
}