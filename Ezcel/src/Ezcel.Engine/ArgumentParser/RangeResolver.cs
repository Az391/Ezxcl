using System;
using System.Text;

namespace Ezcel.Engine.ArgumentParser
{
    public class RangeResolver
    {
        public string ResolveToMarkdownTable(object[,] range)
        {
            if (range == null || range.GetLength(0) == 0 || range.GetLength(1) == 0)
            {
                return string.Empty;
            }

            var sb = new StringBuilder();
            int rows = range.GetLength(0);
            int cols = range.GetLength(1);

            // 构建表头
            sb.Append("| ");
            for (int j = 0; j < cols; j++)
            {
                sb.Append($"列{j + 1} | ");
            }
            sb.AppendLine();

            // 构建分隔线
            sb.Append("| ");
            for (int j = 0; j < cols; j++)
            {
                sb.Append("--- | ");
            }
            sb.AppendLine();

            // 构建数据行
            for (int i = 0; i < rows; i++)
            {
                sb.Append("| ");
                for (int j = 0; j < cols; j++)
                {
                    var value = range[i, j];
                    string cellValue = value?.ToString() ?? string.Empty;
                    sb.Append($"{cellValue} | ");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}