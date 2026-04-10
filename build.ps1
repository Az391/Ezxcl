# Ezcel 构建脚本
# 使用方法：.\build.ps1

param(
    [string]$Configuration = "Release",
    [switch]$CreateZip
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Ezcel 构建脚本" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# 检查 .NET SDK
Write-Host "[1/4] 检查 .NET SDK..." -ForegroundColor Yellow
try {
    $dotnetVersion = dotnet --version
    Write-Host "✓ .NET SDK 版本: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "✗ 未找到 .NET SDK，请先安装 .NET 9.0 SDK" -ForegroundColor Red
    exit 1
}

# 还原依赖
Write-Host ""
Write-Host "[2/4] 还原 NuGet 包..." -ForegroundColor Yellow
dotnet restore Ezcel.sln
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ 还原依赖失败" -ForegroundColor Red
    exit 1
}
Write-Host "✓ 依赖还原完成" -ForegroundColor Green

# 构建项目
Write-Host ""
Write-Host "[3/4] 构建项目 (配置: $Configuration)..." -ForegroundColor Yellow
dotnet build Ezcel.sln --configuration $Configuration --no-restore
if ($LASTEXITCODE -ne 0) {
    Write-Host "✗ 构建失败" -ForegroundColor Red
    exit 1
}
Write-Host "✓ 构建成功" -ForegroundColor Green

# 创建发布包
if ($CreateZip) {
    Write-Host ""
    Write-Host "[4/4] 创建发布包..." -ForegroundColor Yellow
    
    $releaseDir = "release"
    if (Test-Path $releaseDir) {
        Remove-Item -Path $releaseDir -Recurse -Force
    }
    New-Item -ItemType Directory -Path $releaseDir | Out-Null
    
    # 复制构建输出
    $buildOutput = "src\Ezcel.AddIn\bin\$Configuration\net9.0-windows"
    Copy-Item -Path "$buildOutput\*" -Destination $releaseDir -Recurse
    
    # 复制文档和配置
    Copy-Item -Path "appsettings.json" -Destination $releaseDir
    Copy-Item -Path "README.md" -Destination $releaseDir
    Copy-Item -Path "INSTALL.md" -Destination $releaseDir
    Copy-Item -Path "LICENSE" -Destination $releaseDir
    Copy-Item -Path "install.ps1" -Destination $releaseDir
    
    # 创建 ZIP 包
    $version = "1.0.0"
    $zipFile = "Ezcel-v$version.zip"
    if (Test-Path $zipFile) {
        Remove-Item -Path $zipFile -Force
    }
    Compress-Archive -Path "$releaseDir\*" -DestinationPath $zipFile
    
    Write-Host "✓ 发布包创建完成: $zipFile" -ForegroundColor Green
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  构建完成！" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""
Write-Host "构建输出目录: src\Ezcel.AddIn\bin\$Configuration\net9.0-windows" -ForegroundColor White
if ($CreateZip) {
    Write-Host "发布包: $zipFile" -ForegroundColor White
}