using ExcelDna.Integration;
using System;
using System.Collections.Generic;

namespace Ezcel.AddIn.Functions
{
    public static class PromptFunctions
    {
        [ExcelFunction(Description = "使用默认模型发送提示，单值返回", Category = "Ezcel AI")]
        public static object PROMPT(
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return "Hello from PROMPT function!";
        }

        [ExcelFunction(Description = "使用默认模型发送提示，结果向右溢出到同行", Category = "Ezcel AI")]
        public static object PROMPT_TOROW(
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1", "Result 2", "Result 3" } };
        }

        [ExcelFunction(Description = "使用默认模型发送提示，结果向下溢出到同列", Category = "Ezcel AI")]
        public static object PROMPT_TOCOLUMN(
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1" }, { "Result 2" }, { "Result 3" } };
        }

        [ExcelFunction(Description = "使用默认模型发送提示，结果以行列矩阵形式溢出", Category = "Ezcel AI")]
        public static object PROMPT_TORANGE(
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1-1", "Result 1-2" }, { "Result 2-1", "Result 2-2" } };
        }

        [ExcelFunction(Description = "指定模型发送提示（格式：provider/model）", Category = "Ezcel AI")]
        public static object PROMPTMODEL(
            [ExcelArgument(Description = "模型指定（格式：provider/model）")] string model,
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return "Hello from PROMPTMODEL function!";
        }

        [ExcelFunction(Description = "指定模型发送提示，结果向右溢出到同行", Category = "Ezcel AI")]
        public static object PROMPTMODEL_TOROW(
            [ExcelArgument(Description = "模型指定（格式：provider/model）")] string model,
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1", "Result 2", "Result 3" } };
        }

        [ExcelFunction(Description = "指定模型发送提示，结果向下溢出到同列", Category = "Ezcel AI")]
        public static object PROMPTMODEL_TOCOLUMN(
            [ExcelArgument(Description = "模型指定（格式：provider/model）")] string model,
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1" }, { "Result 2" }, { "Result 3" } };
        }

        [ExcelFunction(Description = "指定模型发送提示，结果以行列矩阵形式溢出", Category = "Ezcel AI")]
        public static object PROMPTMODEL_TORANGE(
            [ExcelArgument(Description = "模型指定（格式：provider/model）")] string model,
            [ExcelArgument(Description = "指令文本")] string instruction,
            [ExcelArgument(Description = "可选的单元格引用", Name = "refs", IsOptional = true)] params object[] refs
        )
        {
            return new object[,] { { "Result 1-1", "Result 1-2" }, { "Result 2-1", "Result 2-2" } };
        }
    }
}