using Otus.Teaching.Concurrency.Import.DataAccess.ImportDataTypes;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.ExecuteTypes;

namespace Otus.Teaching.Concurrency.Import.Loader.CommandLine.Parameters;

public class LoaderCommandLineParameters
{
    public ExecuteType ExecuteType { get; set; }

    public ImportDataType ImportDataType { get; set; }

    public int RecordCount { get; set; }

    public int ThreadCount { get; set; }

    public int RetryCount { get; set; }
}