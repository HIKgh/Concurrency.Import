using Otus.Teaching.Concurrency.Import.Core.Generators;
using Otus.Teaching.Concurrency.Import.DataAccess;
using Otus.Teaching.Concurrency.Import.DataAccess.Repositories;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators;

public static class GeneratorFactory
{
    public static IDataGenerator GetXmlGenerator(string fileName, int dataCount)
    {
        return new XmlGenerator(fileName, dataCount);
    }

    public static IDataGenerator GetCsvGenerator(string fileName, int dataCount)
    {
        return new CsvGenerator(fileName, dataCount);
    }

    public static IDataGenerator GetPostgresGenerator(DataContext dbContext, int dataCount)
    {
        return new PostrgesGenerator(new CustomerRepository(dbContext), dataCount);
    }
}