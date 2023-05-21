using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Loaders;

namespace Otus.Teaching.Concurrency.Import.Loader.Loaders;

public class ThreadsDataLoader : IDataLoader
{
    private const int MaxThreadCount = 50;
    private const int MaxRetryCount = 3;
    private readonly int _threadCount;
    private readonly int _retryCount;
    private readonly List<Thread> _threads = new();
    private readonly List<Customer> _customers;

    public ThreadsDataLoader(List<Customer> customers, int threadCount, int retryCount)
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

    public void LoadData()
    {
        Console.WriteLine();
        Console.WriteLine($"Запуск обработки данных Threads {_threadCount}");
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        for (var i = 1; i <= _threadCount; i++)
        {
            _threads.Add(StartHandlerThread(i));
        }
        _threads.ForEach(x => x.Join());

        stopWatch.Stop();
        Console.WriteLine($"Обработка данных Threads завершена за {stopWatch.Elapsed}");
    }

    private Thread StartHandlerThread(int threadKey)
    {
        var handler = new RangeCustomersHandler(_customers, _threadCount, threadKey, _retryCount);
        var thread = new Thread(handler.Handle);
        thread.Start();
        return thread;
    }
}