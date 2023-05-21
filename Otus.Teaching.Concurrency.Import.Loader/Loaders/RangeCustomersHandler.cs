using System;
using System.Collections.Generic;
using System.Threading;
using Otus.Teaching.Concurrency.Import.Core.Entities;

namespace Otus.Teaching.Concurrency.Import.Loader.Loaders;

public class RangeCustomersHandler
{
    private List<Customer>? _customers;
    private readonly FakeCustomerHandler _handler;
    private int _retryCount;

    public RangeCustomersHandler(List<Customer> customers, int threadCount, int threadKey, int retryCount)
    {
        InitCustomers(customers, threadCount, threadKey);
        _handler = new FakeCustomerHandler();
        _retryCount = retryCount;
    }

    private void InitCustomers(List<Customer> customers, int threadCount, int threadKey)
    {
        var listCount = customers.Count;
        var range = listCount / threadCount;
        var mod = listCount % threadCount;
        _customers = threadKey == threadCount
            ? new List<Customer>(customers.GetRange((threadCount - 1) * range, range + mod))
            : new List<Customer>(customers.GetRange((threadKey - 1) * range, range));
    }

    public void Handle()
    {
        Console.WriteLine($"Handler запущен в потоке: {Thread.CurrentThread.ManagedThreadId}");
        while (_retryCount > 0)
        {
            try
            {
                if (_customers is { Count: > 0 })
                {
                    foreach (var customer in _customers)
                    {
                        _handler.Handle(customer);
                    }
                }

                break;
            }
            catch
            {
                Console.WriteLine($"Ошибка в Handler в потоке: {Thread.CurrentThread.ManagedThreadId}. Повторная обработка. Осталось попыток {_retryCount}");
                _retryCount--;
                if (_retryCount == 0)
                {
                    Console.WriteLine($"Повторые обработки завершились с ошибкой в Handler в потоке: {Thread.CurrentThread.ManagedThreadId}.");
                }
            }
        }
        Console.WriteLine($"Handler завершен в потоке: {Thread.CurrentThread.ManagedThreadId}");
    }
}