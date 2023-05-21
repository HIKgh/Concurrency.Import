using System.Collections.Generic;
using Otus.Teaching.Concurrency.Import.Core.Entities;

namespace Otus.Teaching.Concurrency.Import.Core.Repositories;

public interface ICustomerRepository
{
    void AddCustomer(Customer customer);

    void Initialize(List<Customer> customers);

    List<Customer> GetAll();
}