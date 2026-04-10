# Ezcel 安装指南

## 系统要求

- Windows 操作系统
- Excel 2016 或更高版本
- .NET 9.0 运行时

## 安装步骤

### 方法一：手动安装

1. **下载最新版本**
   - 访问 [GitHub Releases](https://github.com/Az391/Ezxcl/releases)
   - 下载最新版本的 `Ezcel-vX.X.X.zip`

2. **解压文件**
   - 将下载的 ZIP 文件解压到您选择的文件夹中
   - 建议解压到 `C:\Program Files\Ezcel\` 或用户目录

3. **加载插件到 Excel**
   - 打开 Excel
   - 点击 "文件" → "选项" → "加载项"
   - 在底部的 "管理" 下拉菜单中选择 "Excel 加载项"，然后点击 "转到"
   - 点击 "浏览" 按钮，导航到解压后的文件夹
   - 选择 `Ezcel-AddIn.xll` 文件（或根据您的 Excel 版本选择相应的 .xll 文件）
   - 点击 "确定" 完成安装

### 方法二：拖拽安装

1. 下载并解压最新版本
2. 打开 Excel
3. 将 `Ezcel-AddIn.xll` 文件直接拖拽到 Excel 窗口中
4. 按照提示完成安装

## 配置

1. **设置 API 密钥**
   - 安装完成后，在 Excel 功能区中找到 "Ezcel" 标签
   - 点击 "设置" 按钮
   - 输入您的 OpenAI API 密钥（或其他 AI 提供商的密钥）
   - 点击 "保存"

2. **修改配置文件**
   - 您也可以直接编辑 `appsettings.json` 文件进行高级配置
   - 配置文件位于插件安装目录

## 验证安装

1. 打开 Excel
2. 在任意单元格中输入 `=PROMPT("你好")`
3. 按回车键，如果看到 "Hello from PROMPT function!" 则表示安装成功

## 卸载

1. 打开 Excel
2. 点击 "文件" → "选项" → "加载项"
3. 在底部的 "管理" 下拉菜单中选择 "Excel 加载项"，然后点击 "转到"
4. 取消勾选 "Ezcel"
5. 点击 "确定"
6. 删除插件文件

## 故障排除

### 插件无法加载
- 确保已安装 .NET 9.0 运行时
- 检查 Excel 版本是否符合要求
- 尝试以管理员身份运行 Excel

### 函数无法使用
- 检查 API 密钥是否正确配置
- 查看 `ezcel.log` 文件获取错误信息
- 确保网络连接正常

### 获取帮助
- 访问 [GitHub Issues](https://github.com/Az391/Ezxcl/issues) 提交问题
- 查看 [README.md](README.md) 了解更多使用信息