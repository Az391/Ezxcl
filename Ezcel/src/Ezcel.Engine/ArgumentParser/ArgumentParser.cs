using System;
using System.Collections.Generic;
using System.Linq;

namespace Ezcel.Engine.ArgumentParser
{
    public class ArgumentParser
    {
        public ParsedArguments Parse(object[] args)
        {
            var result = new ParsedArguments();

            if (args == null || args.Length == 0)
            {
                throw new ArgumentException("至少需要提供指令文本");
            }

            // 解析指令文本
            result.Instruction = args[0]?.ToString() ?? string.Empty;

            // 解析可选的单元格引用
            if (args.Length > 1)
            {
                for (int i = 1; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg != null)
                    {
                        if (arg is object[,] array)
                        {
                            // 处理单元格范围
                            result.References.Add(array);
                        }
                        else
                        {
                            // 处理单个值
                            result.References.Add(new object[,] { { arg } });
                        }
                    }
                }
            }

            return result;
        }

        public ParsedArguments ParseWithModel(object[] args)
        {
            var result = new ParsedArguments();

            if (args == null || args.Length < 2)
            {
                throw new ArgumentException("需要提供模型指定和指令文本");
            }

            // 解析模型指定
            result.ModelSpec = args[0]?.ToString() ?? string.Empty;

            // 解析指令文本
            result.Instruction = args[1]?.ToString() ?? string.Empty;

            // 解析可选的单元格引用
            if (args.Length > 2)
            {
                for (int i = 2; i < args.Length; i++)
                {
                    var arg = args[i];
                    if (arg != null)
                    {
                        if (arg is object[,] array)
                        {
                            // 处理单元格范围
                            result.References.Add(array);
                        }
                        else
                        {
                            // 处理单个值
                            result.References.Add(new object[,] { { arg } });
                        }
                    }
                }
            }

            return result;
        }
    }

    public class ParsedArguments
    {
        public string Instruction { get; set; } = string.Empty;
        public string ModelSpec { get; set; } = string.Empty;
        public List<object[,]> References { get; set; } = new List<object[,]>();
    }
}