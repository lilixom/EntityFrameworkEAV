/// Notice : Code written by Dimitris Papadimitriou - http://www.papadi.gr
/// Code is provided to be used freely but without any warranty of any kind
using System;
using System.ServiceModel;

namespace ConsoleHost
{
    internal class Program
    {
        private static void Main()
        {
            Console.WriteLine("This is the SERVER console");
            Console.WriteLine("Service Started!");
            ServiceHost myServiceHost = new ServiceHost(typeof(TAP.FileService.FileTransferService));
            myServiceHost.Open();
            foreach (Uri address in myServiceHost.BaseAddresses)
                Console.WriteLine("Listening on " + address);
            Console.WriteLine("Click any key to close...");
            Console.ReadKey();

            myServiceHost.Close();
        }

        private static void Start()
        {
        }
    }
}