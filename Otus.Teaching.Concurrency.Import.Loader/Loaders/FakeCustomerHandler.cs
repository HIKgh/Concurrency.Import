using System;
using System.Threading;
using Otus.Teaching.Concurrency.Import.Core.Entities;

namespace Otus.Teaching.Concurrency.Import.Loader.Loaders;

public class FakeCustomerHandler
{
    private readonly int _delay;

    public FakeCustomerHandler(int delay = 1)
    {
        _delay = delay;
    }

    public void Handle(Customer customer)
    {
        //Console.WriteLine($"Обработки контрагента {customer.Id}");
        Thread.Sleep(_delay);
        //Console.WriteLine($"Обработки контрагента {customer.Id} завершена");
    }
}