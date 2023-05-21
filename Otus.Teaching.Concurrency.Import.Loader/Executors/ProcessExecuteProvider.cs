using System;
using System.Diagnostics;
using Otus.Teaching.Concurrency.Import.Loader.CommandLine.Parameters;

namespace Otus.Teaching.Concurrency.Import.Loader.Executors;

public class ProcessExecuteProvider
{
    private const string AppFileName = @"..\..\..\..\Otus.Teaching.Concurrency.Import.DataGenerator.App\bin\Debug\net6.0\Otus.Teaching.Concurrency.Import.DataGenerator.App.exe";

    public void Execute(LoaderCommandLineParameters parameters, string fileName, string directoryName)
    {
        Console.WriteLine("Запуск процесса");
        var processInfo = new ProcessStartInfo
        {
            FileName = AppFileName,
            Arguments = $"{(int) parameters.ImportDataType} {parameters.RecordCount} {fileName} {directoryName}"
        };
        try
        {
            Process process = new Process
            {
                StartInfo = processInfo,
                EnableRaisingEvents = true
            };
            process.Exited += (sender, e) => { Console.WriteLine($"Процесс завершен с кодом {process.ExitCode}"); };
            process.Start();
            process.WaitForExit();
        }
        catch
        {
            Console.WriteLine("Произошла ошибка при запуске процесса");
        }
    }
}