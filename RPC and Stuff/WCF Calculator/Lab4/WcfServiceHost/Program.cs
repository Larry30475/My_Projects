using WcfService;
using WcfServiceHost.Information;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace WcfServiceHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MyData.info();
            Console.WriteLine();

            //URI dla bazowego adresu serwisu
            Uri baseAddress = new Uri("http://172.20.10.10/MyCalculator");

            //Instancja serwisu
            ServiceHost myHost = new ServiceHost(typeof(MyCalculator), baseAddress);

            //Endpoint serwisu
            BasicHttpBinding myBinding = new BasicHttpBinding();
            ServiceEndpoint endpoint1 = myHost.AddServiceEndpoint(typeof(ICalculator), myBinding, "endpoint1");

            //Ustawienie metadanych
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            myHost.Description.Behaviors.Add(smb);

            // Dodanie wiązania WSHttpBinding
            WSHttpBinding binding2 = new WSHttpBinding();
            binding2.Security.Mode = SecurityMode.None;
            ServiceEndpoint endpoint2 = myHost.AddServiceEndpoint(typeof(ICalculator), binding2, "endpoint2");

            try
            {
                Console.WriteLine("\n---> Endpoints:");
                PrintEndpointDetails(endpoint1);
                PrintEndpointDetails(endpoint2);
                //Uruchamienie serwisu
                myHost.Open();
                Console.WriteLine("Service is started and running");
                Console.WriteLine("Press <ENTER> to STOP service...");
                Console.WriteLine();
                Console.ReadLine(); //aby nie konczyc natychmiast
                myHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("Exception occured: {0}", ce.Message);
                myHost.Abort();
            }
        }
        private static void PrintEndpointDetails(ServiceEndpoint endpoint)
        {
            Console.WriteLine("Service endpoint: {0}", endpoint.Name);
            Console.WriteLine("Binding: {0}", endpoint.Binding.ToString());
            Console.WriteLine("ListenUri: {0}", endpoint.ListenUri.ToString());
        }
    }
}
