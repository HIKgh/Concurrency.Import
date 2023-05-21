using System.IO;
using Otus.Teaching.Concurrency.Import.Core.Generators;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators;

public class CsvGenerator : IDataGenerator
{
    private const string delimeter = ";";
    private readonly string _fileName;
    private readonly int _dataCount;

    public CsvGenerator(string fileName, int dataCount)
    {
        _fileName = fileName;
        _dataCount = dataCount;
    }

    public void Generate()
    {
        var customers = RandomCustomerGenerator.Generate(_dataCount);
        using var stream = new StreamWriter(_fileName, false);
        foreach (var customer in customers)
        {
            stream.WriteLine($"{customer.Id}{delimeter}{customer.FullName}{delimeter}{customer.Email}{delimeter}{customer.Phone}{delimeter}");
        }
    }
}