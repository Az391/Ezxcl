此次合并将完整的 Ezcel Excel 插件项目添加到仓库中，包含多个模块和功能。项目实现了 Excel 函数集成、AI 模型调用、缓存系统、配置管理等核心功能，为 Excel 用户提供 AI 能力。
| 文件 | 变更 |
|------|---------|
| Ezcel.sln | - 创建解决方案文件，包含多个项目模块 |
| appsettings.json | - 添加配置文件，定义默认模型、提供商设置和弹性策略 |
| src/Ezcel.AddIn/AddIn.cs | - 实现 Excel 插件入口，配置日志和服务容器 |
| src/Ezcel.AddIn/Forms/SettingsForm.cs | - 添加设置表单，用于配置插件参数 |
| src/Ezcel.AddIn/Functions/PromptFunctions.cs | - 实现多个 Excel 函数，支持不同形式的 AI 提示和结果返回 |
| src/Ezcel.AddIn/Ribbon/Ribbon.cs | - 实现 Excel 功能区，提供插件交互界面 |
| src/Ezcel.Cache/ResponseCache.cs | - 实现缓存系统，提高 AI 响应速度 |
| src/Ezcel.Configuration/ConfigManager/ConfigManager.cs | - 实现配置管理，支持 API 密钥加密 |
| src/Ezcel.Configuration/Models/ConfigModels.cs | - 定义配置模型结构 |
| src/Ezcel.Configuration/SecureStorage/SecureStorage.cs | - 实现安全存储，加密 API 密钥 |
| src/Ezcel.Engine/ArgumentParser/ArgumentParser.cs | - 实现参数解析，处理 Excel 函数参数 |
| src/Ezcel.Engine/AsyncScheduler/AsyncScheduler.cs | - 实现异步调度，管理并发请求 |
| src/Ezcel.Engine/ResultWriter/ResultWriter.cs | - 实现结果写入，处理 AI 响应输出 |
| src/Ezcel.Pipeline/Behaviors/CacheBehavior.cs | - 实现缓存行为，优化性能 |
| src/Ezcel.Pipeline/Behaviors/LoggingBehavior.cs | - 实现日志行为，记录系统操作 |
| src/Ezcel.Pipeline/Behaviors/RateLimitBehavior.cs | - 实现速率限制，防止 API 滥用 |
| src/Ezcel.Pipeline/Behaviors/RetryBehavior.cs | - 实现重试机制，提高系统可靠性 |
| src/Ezcel.Pipeline/Behaviors/UsageTrackingBehavior.cs | - 实现使用跟踪，监控系统使用情况 |
| src/Ezcel.Prompt/OutputFormatter/OutputFormatter.cs | - 实现输出格式化，处理 AI 响应格式 |
| src/Ezcel.Prompt/PromptBuilder/PromptBuilder.cs | - 实现提示构建，生成 AI 提示 |
| src/Ezcel.Prompt/SystemMessages/SystemMessageManager.cs | - 实现系统消息管理，提供上下文信息 |
| src/Ezcel.Providers/Abstractions/IChatClient.cs | - 定义聊天客户端接口 |
| src/Ezcel.Providers/Abstractions/IProviderFactory.cs | - 定义提供商工厂接口 |
| src/Ezcel.Providers/BuiltIn/OpenAIProvider.cs | - 实现 OpenAI 提供商，支持 GPT 模型 |
| src/Ezcel.Providers/Custom/CustomProvider.cs | - 实现自定义提供商，支持第三方 AI 服务 |
| src/Ezcel.Providers/Factory/ProviderFactory.cs | - 实现提供商工厂，管理不同 AI 提供商 |
| src/Ezcel.Tools/Abstractions/IAITool.cs | - 定义 AI 工具接口 |
| src/Ezcel.Tools/MCP/McpClient.cs | - 实现 MCP 客户端，与外部服务交互 |
| src/Ezcel.Tools/Native/FileSearchTool.cs | - 实现文件搜索工具，查找本地文件 |
| tests/Ezcel.Tests/ProviderTests.cs | - 添加提供商测试，验证 AI 服务集成 |