using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Loaders;

namespace Otus.Teaching.Concurrency.Import.Loader.Loaders;

public class ThreadPoolDataLoader : IDataLoader
{
    private const int MaxThreadCount = 50;
    private const int MaxRetryCount = 3;
    private readonly int _threadCount;
    private readonly int _retryCount;
    private readonly List<Customer> _customers;
    private readonly Dictionary<int, ThreadPoolProcessItem> _threadPoolProcessItems = new();

    public ThreadPoolDataLoader(List<Customer> customers, int threadCount, int retryCount)
    {
        if (customers.Count < threadCount)
        {
            threadCount = customers.Count;
        }

        if (threadCount > MaxThreadCount)
        {
            threadCount = MaxThreadCount;
        }

        if (retryCount is <= 0 or > MaxRetryCount)
        {
            retryCount = MaxRetryCount;
        }

        _threadCount = threadCount;
        _retryCount = retryCount;
        _customers = customers;
    }

    private void OnThreadFinished(int threadKey)
    {
    }

    public void LoadData()
    {
        Console.WriteLine();
        Console.WriteLine($"Запуск обработки данных ThreadPool {_threadCount}");
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (var i = 1; i <= _threadCount; i++)
        {
            _threadPoolProcessItems.TryAdd(i, new(i, OnThreadFinished));
            ThreadPool.QueueUserWorkItem(new WaitCallback(StartHandler), _threadPoolProcessItems[i]);
        }
        WaitHandle[] waitHandles = _threadPoolProcessItems.Values.Select(x => x.WaitHandle).ToArray();
        WaitHandle.WaitAll(waitHandles);
        stopWatch.Stop();
        Console.WriteLine($"Обработка данных ThreadPool завершена за {stopWatch.Elapsed}");
    }

    private void StartHandler(object item)
    {
        var processItem = (ThreadPoolProcessItem) item;
        var handler = new RangeCustomersHandler(_customers, _threadCount, (int) processItem.ThreadKey, _retryCount);
        handler.Handle();
        var autoResetEvent = (AutoResetEvent) processItem.WaitHandle;
        processItem.OnFinish();
        autoResetEvent.Set();
    }
}