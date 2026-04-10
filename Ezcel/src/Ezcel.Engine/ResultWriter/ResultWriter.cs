using System;
using System.Collections.Generic;
using System.Linq;

namespace Ezcel.Engine.ResultWriter
{
    public class ResultWriter
    {
        public object WriteSingleValue(string result)
        {
            return result;
        }

        public object WriteToRow(string result)
        {
            // 简单实现：按逗号分割为行
            var values = result.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .ToArray();

            if (values.Length == 0)
            {
                return string.Empty;
            }

            // 创建一行的二维数组
            var resultArray = new object[1, values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                resultArray[0, i] = values[i];
            }

            return resultArray;
        }

        public object WriteToColumn(string result)
        {
            // 简单实现：按换行符分割为列
            var values = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(v => v.Trim())
                .ToArray();

            if (values.Length == 0)
            {
                return string.Empty;
            }

            // 创建一列的二维数组
            var resultArray = new object[values.Length, 1];
            for (int i = 0; i < values.Length; i++)
            {
                resultArray[i, 0] = values[i];
            }

            return resultArray;
        }

        public object WriteToRange(string result)
        {
            // 简单实现：按换行符分割行，按逗号分割列
            var rows = result.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(r => r.Trim())
                .Where(r => !string.IsNullOrEmpty(r))
                .ToArray();

            if (rows.Length == 0)
            {
                return string.Empty;
            }

            // 计算最大列数
            int maxColumns = rows.Max(r => r.Split(',', StringSplitOptions.RemoveEmptyEntries).Length);

            // 创建二维数组
            var resultArray = new object[rows.Length, maxColumns];
            for (int i = 0; i < rows.Length; i++)
            {
                var columns = rows[i].Split(',', StringSplitOptions.RemoveEmptyEntries)
                    .Select(c => c.Trim())
                    .ToArray();

                for (int j = 0; j < columns.Length; j++)
                {
                    resultArray[i, j] = columns[j];
                }
            }

            return resultArray;
        }
    }
}