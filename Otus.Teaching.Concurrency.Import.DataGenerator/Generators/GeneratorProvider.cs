using System;
using System.Diagnostics;
using System.IO;
using Otus.Teaching.Concurrency.Import.Core.Generators;
using Otus.Teaching.Concurrency.Import.DataAccess;
using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;

namespace Otus.Teaching.Concurrency.Import.DataGenerator.Generators;

public class GeneratorProvider
{
    public void Generate(ImportDataType importDataType, int recordCount, string fileName, string directory)
    {
        try
        {
            Console.WriteLine();
            Console.WriteLine($"Запуск генерации данных в формате {importDataType}. Количество записей {recordCount}.");
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            IDataGenerator generator;
            switch (importDataType)
            {
                case ImportDataType.Xml:
                    generator = GeneratorFactory.GetXmlGenerator(Path.Combine(directory, $"{fileName}.xml"),
                        recordCount);
                    generator.Generate();
                    break;
                case ImportDataType.Csv:
                    generator = GeneratorFactory.GetCsvGenerator(Path.Combine(directory, $"{fileName}.csv"),
                        recordCount);
                    generator.Generate();
                    break;
                case ImportDataType.Postgres:
                    using (var dataContext = new DataContext())
                    {
                        generator = GeneratorFactory.GetPostgresGenerator(dataContext, recordCount);
                        generator.Generate();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            stopWatch.Stop();
            Console.WriteLine($"Генерация данных в формате {importDataType} завершена за {stopWatch.Elapsed}");
        }
        catch
        {
            Console.WriteLine("Произошла ошибка при генерации данных");
        }
    }
}