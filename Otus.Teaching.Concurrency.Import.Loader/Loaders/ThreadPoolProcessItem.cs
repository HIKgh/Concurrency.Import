using System;
using System.Threading;

namespace Otus.Teaching.Concurrency.Import.Loader.Loaders;

public class ThreadPoolProcessItem
{
    public int ThreadKey { get; private set; }

    public WaitHandle WaitHandle { get; private set; }

    public Action<int> OnFinished { get; set; }

    public ThreadPoolProcessItem(int threadKey, Action<int> onFinishCallback)
    {
        ThreadKey = threadKey;
        WaitHandle = new AutoResetEvent(false);
        OnFinished = onFinishCallback;
    }

    public void OnFinish()
    {
        OnFinished(ThreadKey);
    }
}