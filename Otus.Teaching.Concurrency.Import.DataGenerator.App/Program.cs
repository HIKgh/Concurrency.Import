using Otus.Teaching.Concurrency.Import.DataGenerator.Generators;
using Otus.Teaching.Concurrency.Import.XmlGenerator.CommandLine.Parsers;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator;

class Program
{
    static void Main(string[] args)
    {
        //Параметры запуска: generator.exe [ТипДанных(1 - xml, 2 - csv, 3 - postgres)] [КоличествоЗаписей] [ИмяФайлаБезРасширения] [Каталог]
        //1 10000 customer
        //Находится в папке относительно loader.exe "..\..\..\..\Otus.Teaching.Concurrency.Import.DataGenerator.App\bin\Debug\net6.0\Otus.Teaching.Concurrency.Import.DataGenerator.App.exe"
        var commandLineParser = new GeneratorCommandLineParser();
        var parameters = commandLineParser.TryValidateAndParseArgs(args);
        if (parameters == null)
        {
            return;
        }

        var generatorProvider = new GeneratorProvider();
        generatorProvider.Generate(parameters.ImportDataType, parameters.RecordCount, parameters.FileName, parameters.DirectoryName);
    }
}