using System.Collections.Generic;
using System.IO;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Parsers;

namespace Otus.Teaching.Concurrency.Import.DataAccess.Parsers;

public class CsvParser : IDataParser<List<Customer>>
{
    private readonly string _fileName;
    private const string delimeter = ";";

    public CsvParser(string fileName, string directory)
    {
        _fileName = Path.Combine(directory, $"{fileName}.csv");
    }

    public List<Customer> Parse()
    {
        var list = new List<Customer>();
        using var reader = new StreamReader(_fileName);
        while (reader.ReadLine() is { } line)
        {
            var words = line.Split(delimeter);
            list.Add(new Customer
            {
                Id = int.Parse(words[0]),
                FullName = words[1],
                Email = words[2],
                Phone = words[3]
            });
        }

        return list;
    }
}