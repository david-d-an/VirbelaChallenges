using System;
using System.Threading.Tasks;

namespace Exercise1.Common.Tasks
{
    public static class TaskConstants<TResult>
    {
        static TaskConstants()
        {
            var tcs = new TaskCompletionSource<TResult>();
            tcs.SetException(new NotImplementedException());
            NotImplemented = tcs.Task;
        }

        public static Task<TResult> NotImplemented { get; private set; }
    }
}