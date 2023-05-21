namespace Otus.Teaching.Concurrency.Import.Core.Parsers;

public interface ICommandLineParser<out T> where T : class
{
    T? TryValidateAndParseArgs(string[]? args);
}