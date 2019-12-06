using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace cli_tool
{
    public static class ProcessUtility
    {
        public static void Start(string processCommand)
        {
            Console.WriteLine($"Starting Process {processCommand} ...");

            // Split the command into process and arguments
            string[] commandList = processCommand.Split(' ');
            string processName = commandList[0];
            string arguments = processCommand.Substring(processCommand.IndexOf(" ") + 1);

            var psi = new ProcessStartInfo {
                FileName = processName,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };

            Process proc = new Process { StartInfo = psi };
            proc.Start();
            Task.WaitAll(Task.Run(() =>
            {
                while (!proc.StandardOutput.EndOfStream)
                {
                    var line = proc.StandardOutput.ReadLine();
                    Console.WriteLine(line);
                }
            }), Task.Run(() =>
            {
                while (!proc.StandardError.EndOfStream)
                {
                    var line = proc.StandardError.ReadLine();
                    Console.WriteLine(line);
                }
            }));

            proc.WaitForExit();
            Console.WriteLine(proc.ExitCode);
        }
    }
}
