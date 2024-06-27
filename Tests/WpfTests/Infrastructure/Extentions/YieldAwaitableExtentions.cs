using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfTests.Infrastructure.Extentions
{
    internal static class YieldAwaitableExtentions
    {
        public static YieldAsyncAwaitable ConfigureAwait(this YieldAwaitable _, bool lockContext)
        {
            return new YieldAsyncAwaitable(lockContext);
        }
    }

    internal struct YieldAsyncAwaitable
    {
        readonly YieldAsyncAwaiter _waiter;
        readonly bool _lockContext;

        public YieldAsyncAwaitable(bool lockContext)
        {
            _lockContext = lockContext;
            _waiter = new YieldAsyncAwaiter(lockContext);
        }

        public YieldAsyncAwaiter GetAwaiter() => _waiter;

        public struct YieldAsyncAwaiter : INotifyCompletion, ICriticalNotifyCompletion
        {

            public bool IsCompleted => false;

            public YieldAsyncAwaiter(bool lockContext)
            {

            }

            public void OnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                throw new NotImplementedException();
            }

            public void GetResult() { }
        }
    }
}
