using System;
using System.Collections.Generic;
using System.Diagnostics;
using Otus.Teaching.Concurrency.Import.Core.Entities;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess;
using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;
using Otus.Teaching.Concurrency.Import.DataAccess.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.Repositories;

namespace Otus.Teaching.Concurrency.Import.Loader.Readers;

public class CustomerDataReader
{
    public List<Customer>? GetAll(ImportDataType importDataType, string fileName, string directoryName)
    {
        Console.WriteLine();
        Console.WriteLine($"Запуск загрузки данных в формате {importDataType}");
        var stopWatch = new Stopwatch();
        stopWatch.Start();
        try
        {
            IDataParser<List<Customer>> parser;
            switch (importDataType)
            {
                case ImportDataType.Xml:
                    parser = new XmlParser(fileName, directoryName);
                    return parser.Parse();
                case ImportDataType.Csv:
                    parser = new CsvParser(fileName, directoryName);
                    return parser.Parse();
                case ImportDataType.Postgres:
                    using (var dataContext = new DataContext())
                    {
                        parser = new PostgresParser(new CustomerRepository(dataContext));
                        return parser.Parse();
                    }
                default:
                    return null;
            }
        }
        catch
        {
            Console.WriteLine("Произошла ошибка при чтении данных");
            return null;
        }
        finally
        {
            stopWatch.Stop();
            Console.WriteLine($"Загрузка данных в формате {importDataType} завершена за {stopWatch.Elapsed}");
        }
    }
}