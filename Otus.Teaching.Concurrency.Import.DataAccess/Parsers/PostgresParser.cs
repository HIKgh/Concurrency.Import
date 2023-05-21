using System.Collections.Generic;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.Core.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers;

public class PostgresParser : IDataParser<List<Customer>>
{
    private readonly ICustomerRepository _repository;

    public PostgresParser(ICustomerRepository repository)
    {
        _repository = repository;
    }

    public List<Customer> Parse()
    {
        return _repository.GetAll();
    }
}