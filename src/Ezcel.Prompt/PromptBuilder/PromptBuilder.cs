using Ezcel.Prompt.OutputFormatter;
using Ezcel.Prompt.SystemMessages;
using Ezcel.Providers.Abstractions;
using System.Collections.Generic;

namespace Ezcel.Prompt.PromptBuilder
{
    public class PromptBuilder
    {
        private readonly SystemMessageManager _systemMessageManager;
        private readonly OutputFormatter _outputFormatter;

        public PromptBuilder()
        {
            _systemMessageManager = new SystemMessageManager();
            _outputFormatter = new OutputFormatter();
        }

        public List<ChatMessage> BuildMessages(string instruction, string systemMessageType = "default")
        {
            var messages = new List<ChatMessage>();

            // 添加系统消息
            var systemMessage = _systemMessageManager.GetSystemMessage(systemMessageType);
            messages.Add(new ChatMessage
            {
                Role = "system",
                Content = systemMessage
            });

            // 添加用户消息
            messages.Add(new ChatMessage
            {
                Role = "user",
                Content = instruction
            });

            return messages;
        }

        public List<ChatMessage> BuildMessagesWithData(string instruction, string data, string systemMessageType = "default")
        {
            var messages = new List<ChatMessage>();

            // 添加系统消息
            var systemMessage = _systemMessageManager.GetSystemMessage(systemMessageType);
            messages.Add(new ChatMessage
            {
                Role = "system",
                Content = systemMessage
            });

            // 添加用户消息，包含数据
            var userMessage = $"{instruction}\n\n以下是数据：\n{data}";
            messages.Add(new ChatMessage
            {
                Role = "user",
                Content = userMessage
            });

            return messages;
        }

        public List<ChatMessage> BuildMessagesWithFormat(string instruction, OutputFormatter.OutputType outputType, string systemMessageType = "default")
        {
            // 格式化指令
            var formattedInstruction = _outputFormatter.FormatInstruction(instruction, outputType);
            return BuildMessages(formattedInstruction, systemMessageType);
        }

        public List<ChatMessage> BuildMessagesWithDataAndFormat(string instruction, string data, OutputFormatter.OutputType outputType, string systemMessageType = "default")
        {
            // 格式化指令
            var formattedInstruction = _outputFormatter.FormatInstruction(instruction, outputType);
            return BuildMessagesWithData(formattedInstruction, data, systemMessageType);
        }
    }
}