using System;
using System.Numerics;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using WcfClient.Information;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            MyData.info();

            Console.WriteLine("\nClient is started");

            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseaddress = new Uri("http://172.20.10.10/MyCalculator/endpoint1");
            EndpointAddress eAddress = new EndpointAddress(baseaddress);
            ChannelFactory<ICalculator> myCF = new ChannelFactory<ICalculator>(binding, eAddress);
            ICalculator myClient = myCF.CreateChannel();

            //CalculatorClient myClient = new CalculatorClient("WSHttpBinding_ICalculator");


            Console.WriteLine("\nWybierz operację:");
            Console.WriteLine("1. Dodawanie");
            Console.WriteLine("2. Odejmowanie");
            Console.WriteLine("3. Mnożenie");
            Console.WriteLine("4. Dzielenie");
            Console.WriteLine("5. Modulo");
            Console.WriteLine("6. Znalezienie najwyższej liczby pierwszej i policzenie liczby liczb pierwszych");
            Console.WriteLine("0. Exit");

            int option = -1;
            Task<PrimeNumbersResult> resultAsynchronous = null;
            //Task<ServiceReference1.PrimeNumbersResult> resultAsynchronous = null;
            while (option != 0)
            {
                if (resultAsynchronous != null && resultAsynchronous.IsCompleted)
                {
                    var result = await resultAsynchronous;
                    Console.WriteLine("Liczenie liczb pierwszych zostało zakończone. Oto wyniki");
                    Console.WriteLine($"Najwieksza liczba pierwsza {result.LargestPrime}");
                    Console.WriteLine($"Liczba liczb pierwszych {result.Count}");
                }

                Console.Write("Wybierz opcję: ");
                option = GetNumberFromUser();
                switch (option)
                {
                    case 1:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetNumberFromUser();
                            int result = myClient.iAdd(n1, n2);
                            Console.WriteLine($"{n1} + {n2} = {result}");
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 2:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetNumberFromUser();
                            int result = myClient.iSub(n1, n2);
                            Console.WriteLine($"{n1} - {n2} = {result}");
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 3:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetNumberFromUser();
                            int result = myClient.iMul(n1, n2);
                            Console.WriteLine($"{n1} * {n2} = {result}");
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 4:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetNumberFromUser();
                            int result = myClient.iDiv(n1, n2);
                            Console.WriteLine($"{n1} / {n2} = {result}");
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 5:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetNumberFromUser();
                            int result = myClient.iMod(n1, n2);
                            Console.WriteLine($"{n1} % {n2} = {result}");
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    case 6:
                        try
                        {
                            Console.Write("Podaj pierwszą liczbę: ");
                            var n1 = GetLargeNumberFromUser();
                            Console.Write("Podaj drugą liczbę: ");
                            var n2 = GetLargeNumberFromUser();
                            resultAsynchronous = myClient.CountAndFindMaxPrimeNumbersAsync(n1, n2);
                            
                            Console.WriteLine("Kod się wykonuje");
                            
                        }
                        catch (FaultException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;
                    default:
                        Console.WriteLine("Niepoprawna opcja");
                        break;
                }
            }

            Console.WriteLine("\nPress <ENTER> to STOP client");
            Console.WriteLine();
            Console.ReadLine();

            ((IClientChannel)myClient).Close();
            // myClient.Close();


            Console.WriteLine("Client closed");
        }

        private static int GetNumberFromUser()
        {
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Niepoprawny liczba. Wpisz ponownie: ");
                return GetNumberFromUser();
            }
            return number;
        }

        private static BigInteger GetLargeNumberFromUser()
        {
            BigInteger number;
            if (!BigInteger.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Niepoprawny liczba. Wpisz ponownie: ");
                return GetNumberFromUser();
            }
            return number;
        }
    }
}
