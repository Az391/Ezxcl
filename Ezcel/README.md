# Ezcel - Excel AI 插件

Ezcel 是一个功能强大的 Excel 插件，为 Excel 用户提供 AI 能力，通过简单的函数调用即可访问先进的 AI 模型。

## 功能特点

### 核心功能
- **AI 函数集成**：通过简单的 Excel 函数调用 AI 模型
- **多种返回格式**：支持单值、行溢出、列溢出和矩阵溢出
- **多模型支持**：内置支持 OpenAI GPT 模型，可扩展自定义提供商
- **缓存系统**：提高响应速度，减少 API 调用
- **配置管理**：安全存储 API 密钥，支持自定义配置
- **弹性策略**：内置速率限制、重试机制和并发控制

### 支持的函数

| 函数 | 描述 | 示例 |
|------|------|------|
| `PROMPT` | 使用默认模型发送提示，单值返回 | `=PROMPT("分析这些数据", A1:C10)` |
| `PROMPT_TOROW` | 使用默认模型发送提示，结果向右溢出到同行 | `=PROMPT_TOROW("生成三个建议", A1:B5)` |
| `PROMPT_TOCOLUMN` | 使用默认模型发送提示，结果向下溢出到同列 | `=PROMPT_TOCOLUMN("列出五个关键点", A1)` |
| `PROMPT_TORANGE` | 使用默认模型发送提示，结果以行列矩阵形式溢出 | `=PROMPT_TORANGE("生成产品矩阵", A1:B10)` |
| `PROMPTMODEL` | 指定模型发送提示（格式：provider/model） | `=PROMPTMODEL("openai/gpt-4o", "分析这些数据", A1:C10)` |
| `PROMPTMODEL_TOROW` | 指定模型发送提示，结果向右溢出到同行 | `=PROMPTMODEL_TOROW("openai/gpt-4o", "生成三个建议", A1:B5)` |
| `PROMPTMODEL_TOCOLUMN` | 指定模型发送提示，结果向下溢出到同列 | `=PROMPTMODEL_TOCOLUMN("openai/gpt-4o", "列出五个关键点", A1)` |
| `PROMPTMODEL_TORANGE` | 指定模型发送提示，结果以行列矩阵形式溢出 | `=PROMPTMODEL_TORANGE("openai/gpt-4o", "生成产品矩阵", A1:B10)` |

## 安装方法

1. **克隆仓库**
   ```bash
   git clone https://github.com/your-repo/ezcel.git
   cd ezcel
   ```

2. **构建项目**
   - 使用 Visual Studio 打开 `Ezcel.sln` 解决方案
   - 构建整个解决方案

3. **安装插件**
   - 构建完成后，Excel 会自动加载插件
   - 或者手动将生成的 `.xll` 文件加载到 Excel 中

## 配置说明

1. **API 密钥配置**
   - 打开 Excel，点击 Ezcel 功能区的 "设置" 按钮
   - 在设置表单中输入您的 API 密钥
   - 密钥会被安全加密存储

2. **配置文件**
   编辑 `appsettings.json` 文件可以修改默认配置：

   ```json
   {
     "GlobalSettings": {
       "DefaultProvider": "openai",
       "DefaultModel": "gpt-4o-mini",
       "DefaultTemperature": 0.0,
       "MaxOutputTokens": 8192,
       "EnableCache": true,
       "EnableFileLogging": true
     },
     "Providers": {
       "openai": {
         "BaseUrl": "https://api.openai.com/v1",
         "ApiKey": "",
         "ModelId": "gpt-4o-mini",
         "Type": 0
       }
     },
     "Resilience": {
       "TokenBucketRate": 2,
       "MaxConcurrentRequests": 4,
       "MaxRetries": 5,
       "HttpTimeoutSeconds": 600
     }
   }
   ```

## 项目结构

```
Ezcel/
├── Ezcel.sln                      # 解决方案文件
├── appsettings.json               # 配置文件
├── src/
│   ├── Ezcel.AddIn/               # Excel 插件主项目
│   │   ├── AddIn.cs               # 插件入口
│   │   ├── Functions/             # Excel 函数实现
│   │   ├── Forms/                 # 表单界面
│   │   └── Ribbon/                # Excel 功能区
│   ├── Ezcel.Cache/               # 缓存系统
│   ├── Ezcel.Configuration/       # 配置管理
│   ├── Ezcel.Engine/              # 核心引擎
│   ├── Ezcel.Pipeline/            # 处理管道
│   ├── Ezcel.Prompt/              # 提示处理
│   ├── Ezcel.Providers/           # AI 提供商
│   └── Ezcel.Tools/               # 工具类
└── tests/                         # 测试项目
```

## 使用示例

### 基本用法

1. **文本生成**
   ```excel
   =PROMPT("生成一个销售报告摘要", A1:E10)
   ```

2. **数据分析**
   ```excel
   =PROMPT_TOROW("分析这些销售数据，提供三个改进建议", A1:C10)
   ```

3. **使用特定模型**
   ```excel
   =PROMPTMODEL("openai/gpt-4o", "详细分析这些数据趋势", A1:D20)
   ```

### 高级用法

1. **结合 Excel 公式**
   ```excel
   =PROMPT("分析" & A1 & "月份的销售数据", B2:E10)
   ```

2. **使用矩阵返回**
   ```excel
   =PROMPT_TORANGE("为这些产品生成价格策略矩阵", A1:B5)
   ```

## 性能优化

- **启用缓存**：默认情况下缓存已启用，可提高重复查询的响应速度
- **合理设置并发**：根据您的 API 限制调整并发请求数
- **使用适当的模型**：对于简单任务使用轻量级模型，复杂任务使用更强大的模型

## 故障排除

1. **API 密钥错误**
   - 检查 API 密钥是否正确
   - 确保密钥有足够的权限

2. **函数返回错误**
   - 检查网络连接
   - 查看 `ezcel.log` 文件获取详细错误信息
   - 确认 API 提供商服务正常

3. **性能问题**
   - 减少单次请求的数据量
   - 启用缓存
   - 调整并发设置

## 贡献指南

1. **Fork 项目**
2. **创建功能分支**
3. **提交更改**
4. **创建 Pull Request**

## 许可证

本项目采用 MIT 许可证。详见 [LICENSE](LICENSE) 文件。

## 联系方式

如有问题或建议，请通过以下方式联系：
- GitHub Issues：[https://github.com/your-repo/ezcel/issues](https://github.com/your-repo/ezcel/issues)
- 电子邮件：contact@ezcel.com

---

**Ezcel - 让 Excel 更智能**