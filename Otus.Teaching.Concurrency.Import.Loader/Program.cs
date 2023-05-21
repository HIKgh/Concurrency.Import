using System;
using System.Threading;
using Otus.Teaching.Concurrency.Import.DataGenerator.Generators;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.ExecuteTypes;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.Parsers;
using Otus.Teaching.Concurrency.Import.Loader.Executors;
using Otus.Teaching.Concurrency.Import.Loader.Loaders;
using Otus.Teaching.Concurrency.Import.Loader.Readers;

namespace Otus.Teaching.Concurrency.Import.Loader;

class Program
{
    private const string FileName = "customer";
    private const string DirectoryName = @"c:\temp\";

    static void Main(string[] args)
    {
        //Параметры запуска: loader.exe[ТипЗапуска(0 - метод, 1 - процесс)][ТипДанных(1 - xml, 2 - csv, 3 - postgres)][КоличествоЗаписей][КоличествоПотоков][КоличествоПовторов]
        //1 1 1000 3 3
        //Файл генерируется в папку c:\temp\
        Console.Clear();
        var commandLineParser = new LoaderCommandLineParser();
        var parameters = commandLineParser.TryValidateAndParseArgs(args);
        if (parameters == null)
        {
            return;
        }

        switch (parameters.ExecuteType)
        {
            case ExecuteType.Method:
                var generatorProvider = new GeneratorProvider();
                generatorProvider.Generate(parameters.ImportDataType, parameters.RecordCount, FileName, DirectoryName);
                break;
            case ExecuteType.Process:
                var processExecuteProvider = new ProcessExecuteProvider();
                processExecuteProvider.Execute(parameters, FileName, DirectoryName);
                
                break;
            default: return;
        }

        var reader = new CustomerDataReader();
        var list = reader.GetAll(parameters.ImportDataType, FileName, DirectoryName);
        if (list == null)
        {
            return;
        }

        var threadLoader = new ThreadsDataLoader(list, parameters.ThreadCount, parameters.RetryCount);
        threadLoader.LoadData();

        var threadPoolLoader = new ThreadPoolDataLoader(list, parameters.ThreadCount, parameters.RetryCount);
        threadPoolLoader.LoadData();

        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }
}