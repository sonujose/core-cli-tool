using System;

namespace cli_tool
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to cli tool... --> Choose an option");

            //Initiating the interactive menu...
            var menu = new EasyConsoleCore.Menu()
                           .Add("Run Microservice", () => ListKubernetesPods())
                           .Add("Generate Template", () => ListDockerProcess());
            menu.Display();
        }

        private static void ListKubernetesPods()
        {
            ProcessUtility.Start("kubectl get pods");
        }

        private static void ListDockerProcess()
        {
            ProcessUtility.Start("docker ps");
        }
    }
}
