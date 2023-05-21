using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;

namespace Otus.Teaching.Concurrency.Import.XmlGenerator.CommandLine.Parameters;

public class GeneratorCommandLineParameters
{
    public ImportDataType ImportDataType { get; set; }

    public int RecordCount { get; set; }

    public string FileName { get; set; }

    public string DirectoryName { get; set; }
}