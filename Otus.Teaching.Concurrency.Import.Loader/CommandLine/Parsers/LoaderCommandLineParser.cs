using System;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.ExecuteTypes;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.Parameters;

namespace Otus.Teaching.Concurrency.Import.Loader.CommandLine.Parsers;

public class LoaderCommandLineParser : ICommandLineParser<LoaderCommandLineParameters>
{
    private const string ExecuteParamsStr = "Параметры запуска: loader.exe";
    private const string ExecuteTypeStr = "ТипЗапуска(0 - метод, 1 - процесс)";
    private const string DataTypeStr = "ТипДанных(1 - xml, 2 - csv, 3 - postgres)";
    private const string RecordCountStr = "КоличествоЗаписей";
    private const string ThreadCountStr = "КоличествоПотоков";
    private const string RetryCountStr = "КоличествоПовторов";
    private const string IsRequiredStr = "Необходимо задать";
    private const int MaxThreadCount = 50;
    private const int MinThreadCount = 1;
    private const int MaxRetryCount = 3;

    public LoaderCommandLineParameters? TryValidateAndParseArgs(string[]? args)
    {
        if (args is not { Length: 5 } || args.Length > 0 && args[0] == "/?")
        {
            Console.WriteLine($"{ExecuteParamsStr} [{ExecuteTypeStr}] [{DataTypeStr}] [{RecordCountStr}] [{ThreadCountStr}] [{RetryCountStr}]");
            return null;
        }

        if (!(int.TryParse(args[0], out var executeType) && Enum.IsDefined(typeof(ExecuteType), executeType)))
        {
            Console.WriteLine($"{IsRequiredStr} {ExecuteTypeStr}");
            return null;
        }

        if (!(int.TryParse(args[1], out var importDataType) && Enum.IsDefined(typeof(ImportDataType), importDataType)))
        {
            Console.WriteLine($"{IsRequiredStr} {DataTypeStr}");
            return null;
        }

        if (!int.TryParse(args[2], out var recordCount))
        {
            Console.WriteLine($"{IsRequiredStr} {RecordCountStr}");
            return null;
        }

        if (!(int.TryParse(args[3], out var threadCount) && threadCount > 0))
        {
            Console.WriteLine($"{IsRequiredStr} {ThreadCountStr}");
            return null;
        }

        if (threadCount > recordCount || threadCount > MaxThreadCount)
        {
            threadCount = MinThreadCount;
        }

        if (!(int.TryParse(args[4], out var retryCount) && retryCount > 0))
        {
            Console.WriteLine($"{IsRequiredStr} {RetryCountStr}");
            return null;
        }

        if (retryCount > MaxRetryCount)
        {
            retryCount = MaxRetryCount;
        }

        var result = new LoaderCommandLineParameters
        {
            ExecuteType = (ExecuteType) executeType,
            ImportDataType = (ImportDataType) importDataType,
            RecordCount = recordCount,
            ThreadCount = threadCount,
            RetryCount = retryCount
        };

        return result;
    }
}