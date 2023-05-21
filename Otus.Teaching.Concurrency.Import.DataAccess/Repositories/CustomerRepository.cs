using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _dataContext;

    public CustomerRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public void AddCustomer(Customer customer)
    {
        //Add customer to data source   
    }

    public void Initialize(List<Customer> customers)
    {
        _dataContext.Database.EnsureDeleted();
        _dataContext.Database.EnsureCreated();
        _dataContext.Customers.AddRange(customers);
        _dataContext.SaveChanges();
    }

    public List<Customer> GetAll()
    {
        return _dataContext.Customers.AsNoTracking().ToArray().ToList();
    }
}