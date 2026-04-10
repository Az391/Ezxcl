using ExcelDna.Integration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ezcel.Engine.AsyncScheduler
{
    public class AsyncScheduler
    {
        private static readonly Dictionary<string, Task<object>> _runningTasks = new Dictionary<string, Task<object>>();

        public object RunAsync(Func<Task<object>> asyncOperation, string cellAddress)
        {
            // 检查是否有正在运行的任务
            if (_runningTasks.TryGetValue(cellAddress, out var existingTask))
            {
                if (!existingTask.IsCompleted)
                {
                    return ExcelError.ExcelErrorGettingData;
                }
                _runningTasks.Remove(cellAddress);
            }

            // 创建新任务
            var task = asyncOperation();
            _runningTasks[cellAddress] = task;

            // 任务完成后重新计算单元格
            task.ContinueWith(t =>
            {
                _runningTasks.Remove(cellAddress);
                ExcelAsyncUtil.Run("RefreshCell", null, () =>
                {
                    try
                    {
                        ExcelReference.FromR1C1(cellAddress).Invalidate();
                    }
                    catch { }
                });
            });

            return ExcelError.ExcelErrorGettingData;
        }

        public void CancelTask(string cellAddress)
        {
            if (_runningTasks.TryGetValue(cellAddress, out var task))
            {
                // 这里可以添加取消逻辑
                _runningTasks.Remove(cellAddress);
            }
        }
    }
}