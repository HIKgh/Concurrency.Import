using System;
using Otus.Teaching.Concurrency.Import.Core.Parsers;
using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;
using Otus.Teaching.Concurrency.Import.XmlGenerator.CommandLine.Parameters;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator.CommandLine.Parsers;

public class GeneratorCommandLineParser : ICommandLineParser<GeneratorCommandLineParameters>
{
    private const string ExecuteParamsStr = "Параметры запуска: generator.exe";
    private const string DataTypeStr = "ТипДанных(1 - xml, 2 - csv, 3 - postgres)";
    private const string RecordCountStr = "КоличествоЗаписей";
    private const string FileNameStr = "ИмяФайлаБезРасширения";
    private const string DirectoryNameStr = "Каталог";
    private const string IsRequiredStr = "Необходимо задать";

    public GeneratorCommandLineParameters? TryValidateAndParseArgs(string[]? args)
    {
        if (args == null
            || args.Length > 0 && args[0] == "/?"
            || args.Length is 0 or 1 or > 4)
        {
            Console.WriteLine($"{ExecuteParamsStr} [{DataTypeStr}] [{RecordCountStr}] [{FileNameStr}]");
            return null;
        }

        if (!(int.TryParse(args[0], out var importDataType) && Enum.IsDefined(typeof(ImportDataType), importDataType)))
        {
            Console.WriteLine($"{IsRequiredStr} {DataTypeStr}");
            return null;
        }

        if (!int.TryParse(args[1], out var recordCount))
        {
            Console.WriteLine($"{IsRequiredStr} {RecordCountStr}");
            return null;
        }

        if (((ImportDataType) importDataType == ImportDataType.Csv || (ImportDataType) importDataType == ImportDataType.Xml)
            && args.Length < 3)
        {
            Console.WriteLine($"{IsRequiredStr} {FileNameStr}");
            return null;
        }

        var fileName = args[2];

        if (((ImportDataType)importDataType == ImportDataType.Csv || (ImportDataType) importDataType == ImportDataType.Xml)
            && args.Length < 4)
        {
            Console.WriteLine($"{IsRequiredStr} {DirectoryNameStr}");
            return null;
        }

        var directoryName  = args[3];

        var result = new GeneratorCommandLineParameters
        {
            ImportDataType = (ImportDataType) importDataType,
            RecordCount = recordCount,
            FileName = fileName,
            DirectoryName = directoryName
        };

        return result;
    }
}