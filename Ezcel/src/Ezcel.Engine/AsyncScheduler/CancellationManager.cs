using System;
using System.Collections.Generic;
using System.Threading;

namespace Ezcel.Engine.AsyncScheduler
{
    public class CancellationManager
    {
        private static readonly Dictionary<string, CancellationTokenSource> _ctsMap = new Dictionary<string, CancellationTokenSource>();

        public CancellationToken GetToken(string cellAddress)
        {
            if (!_ctsMap.TryGetValue(cellAddress, out var cts))
            {
                cts = new CancellationTokenSource();
                _ctsMap[cellAddress] = cts;
            }

            return cts.Token;
        }

        public void Cancel(string cellAddress)
        {
            if (_ctsMap.TryGetValue(cellAddress, out var cts))
            {
                try
                {
                    cts.Cancel();
                }
                catch { }
                _ctsMap.Remove(cellAddress);
            }
        }

        public void Cleanup(string cellAddress)
        {
            if (_ctsMap.TryGetValue(cellAddress, out var cts))
            {
                try
                {
                    cts.Dispose();
                }
                catch { }
                _ctsMap.Remove(cellAddress);
            }
        }
    }
}