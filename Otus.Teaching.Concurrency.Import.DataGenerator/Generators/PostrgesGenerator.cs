using Otus.Teaching.Concurrency.Import.Core.Generators;
using Otus.Teaching.Concurrency.Import.Core.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators;

public class PostrgesGenerator : IDataGenerator
{
    private readonly ICustomerRepository _repository;
    private readonly int _dataCount;

    public PostrgesGenerator(ICustomerRepository repository, int dataCount)
    {
        _repository = repository;
        _dataCount = dataCount;
    }

    public void Generate()
    {
        var customers = RandomCustomerGenerator.Generate(_dataCount);
        _repository.Initialize(customers);
    }
}