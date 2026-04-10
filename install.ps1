# Ezcel 安装脚本
# 使用方法：.\install.ps1

param(
    [switch]$Uninstall
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "  Ezcel 安装脚本" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

function Check-ExcelInstalled {
    Write-Host "[1/5] 检查 Excel 安装状态..." -ForegroundColor Yellow
    
    $excelPaths = @(
        "$env:ProgramFiles\Microsoft Office\root\Office16\EXCEL.EXE",
        "$env:ProgramFiles (x86)\Microsoft Office\root\Office16\EXCEL.EXE",
        "$env:ProgramFiles\Microsoft Office\Office16\EXCEL.EXE",
        "$env:ProgramFiles (x86)\Microsoft Office\Office16\EXCEL.EXE",
        "$env:ProgramFiles\Microsoft Office\root\Office15\EXCEL.EXE",
        "$env:ProgramFiles (x86)\Microsoft Office\root\Office15\EXCEL.EXE"
    )
    
    foreach ($path in $excelPaths) {
        if (Test-Path $path) {
            Write-Host "✓ 找到 Excel 安装: $path" -ForegroundColor Green
            return $true
        }
    }
    
    Write-Host "✗ 未找到 Excel 安装" -ForegroundColor Red
    Write-Host "请先安装 Microsoft Excel 2013 或更高版本" -ForegroundColor Yellow
    return $false
}

function Check-DotNetRuntime {
    Write-Host "[2/5] 检查 .NET 运行时..." -ForegroundColor Yellow
    
    try {
        $dotnetVersion = dotnet --version
        Write-Host "✓ .NET 运行时版本: $dotnetVersion" -ForegroundColor Green
        return $true
    } catch {
        Write-Host "✗ 未找到 .NET 运行时" -ForegroundColor Red
        Write-Host "请安装 .NET 9.0 运行时" -ForegroundColor Yellow
        Write-Host "下载链接: https://dotnet.microsoft.com/download/dotnet/9.0" -ForegroundColor Yellow
        return $false
    }
}

function Get-ExcelAddInPath {
    $userProfile = $env:USERPROFILE
    $addInPath = "$userProfile\AppData\Roaming\Microsoft\AddIns"
    
    if (-not (Test-Path $addInPath)) {
        New-Item -ItemType Directory -Path $addInPath -Force | Out-Null
    }
    
    return $addInPath
}

function Install-AddIn {
    Write-Host "[3/5] 安装 Ezcel 插件..." -ForegroundColor Yellow
    
    $addInPath = Get-ExcelAddInPath
    $currentDir = Get-Location
    
    # 复制插件文件
    $filesToCopy = @(
        "Ezcel-AddIn.xll",
        "Ezcel-AddIn.dna",
        "appsettings.json",
        "README.md",
        "INSTALL.md",
        "LICENSE"
    )
    
    foreach ($file in $filesToCopy) {
        if (Test-Path $file) {
            Copy-Item -Path $file -Destination $addInPath -Force
            Write-Host "✓ 复制 $file 到 $addInPath" -ForegroundColor Green
        } else {
            Write-Host "⚠ 未找到文件: $file" -ForegroundColor Yellow
        }
    }
    
    # 复制依赖文件
    if (Test-Path "runtimes") {
        Copy-Item -Path "runtimes" -Destination $addInPath -Recurse -Force
        Write-Host "✓ 复制运行时依赖" -ForegroundColor Green
    }
    
    if (Test-Path "lib") {
        Copy-Item -Path "lib" -Destination $addInPath -Recurse -Force
        Write-Host "✓ 复制库依赖" -ForegroundColor Green
    }
    
    Write-Host "✓ 插件安装完成" -ForegroundColor Green
}

function Register-AddIn {
    Write-Host "[4/5] 注册插件到 Excel..." -ForegroundColor Yellow
    
    $addInPath = Get-ExcelAddInPath
    $addInFile = "$addInPath\Ezcel-AddIn.xll"
    
    if (Test-Path $addInFile) {
        Write-Host "✓ 插件已注册到 Excel" -ForegroundColor Green
        Write-Host "请打开 Excel 并在 '文件 -> 选项 -> 加载项' 中确认 Ezcel 插件已启用" -ForegroundColor Yellow
    } else {
        Write-Host "✗ 插件文件未找到: $addInFile" -ForegroundColor Red
    }
}

function Uninstall-AddIn {
    Write-Host "[1/3] 卸载 Ezcel 插件..." -ForegroundColor Yellow
    
    $addInPath = Get-ExcelAddInPath
    
    # 删除插件文件
    $filesToRemove = @(
        "Ezcel-AddIn.xll",
        "Ezcel-AddIn.dna",
        "appsettings.json",
        "README.md",
        "INSTALL.md",
        "LICENSE"
    )
    
    foreach ($file in $filesToRemove) {
        $filePath = "$addInPath\$file"
        if (Test-Path $filePath) {
            Remove-Item -Path $filePath -Force
            Write-Host "✓ 删除 $file" -ForegroundColor Green
        }
    }
    
    # 删除依赖文件夹
    $foldersToRemove = @("runtimes", "lib")
    foreach ($folder in $foldersToRemove) {
        $folderPath = "$addInPath\$folder"
        if (Test-Path $folderPath) {
            Remove-Item -Path $folderPath -Recurse -Force
            Write-Host "✓ 删除 $folder 文件夹" -ForegroundColor Green
        }
    }
    
    Write-Host "✓ 插件卸载完成" -ForegroundColor Green
}

if ($Uninstall) {
    Uninstall-AddIn
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Cyan
    Write-Host "  卸载完成！" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Cyan
} else {
    if (Check-ExcelInstalled -and Check-DotNetRuntime) {
        Install-AddIn
        Register-AddIn
        
        Write-Host ""
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host "  安装完成！" -ForegroundColor Green
        Write-Host "========================================" -ForegroundColor Cyan
        Write-Host ""
        Write-Host "现在可以在 Excel 中使用 Ezcel 插件了！" -ForegroundColor White
        Write-Host "在 Excel 单元格中输入 =PROMPT("你好") 测试插件是否正常工作" -ForegroundColor White
    }
}
