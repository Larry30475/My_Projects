using System;
using System.ServiceModel;
using System.Threading.Tasks;
using WcfClient.Services;
using WcfClient.ServiceReference1;

namespace WcfClient
{
    internal class Program
    {
        static public int GetNumberFromUser()
        {
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input.");
                return GetNumberFromUser();
            }
            return number;
        }

        static async Task Main(string[] args)
        {
            MyData.info();
            Console.WriteLine("... Klient zaczął się");

            //BasicHttpBinding myBinding = new BasicHttpBinding();
            //Uri baseAddress = new Uri("http://localhost:8080/DatabaseService");
            //EndpointAddress eAddress = new EndpointAddress(baseAddress);
            //ChannelFactory<IDatabaseService> myCF = new ChannelFactory<IDatabaseService>(myBinding, eAddress);
            //var basicClient = myCF.CreateChannel();
            var wsClient = new DatabaseServiceClient("WSHttpBinding_IDatabaseService");

            int operation = -1;
            do
            {
                Console.WriteLine("Wybierz operację:");
                Console.WriteLine("1. Pokaż wszystkich użytkowników");
                Console.WriteLine("2. Dodaj użytkownika");
                Console.WriteLine("3. Zdobądź dane użytkownika");
                Console.WriteLine("4. Aktualizuj użytkownika");
                Console.WriteLine("5. Skasuj użytkownika");
                Console.WriteLine("6. Zdobądź użytkowników o danym imieniu");
                Console.WriteLine("7. Zdobądź rozmiar bazy");
                Console.WriteLine("0. Zakończ");

                if (!int.TryParse(Console.ReadLine(), out operation))
                {
                    Console.WriteLine("Niepoprawny input.\n");
                    operation = -1;
                    continue;
                }
                Console.WriteLine();

                try
                {
                    int id;
                    string name;
                    int age;
                    string email;
                    switch (operation)
                    {
                        case 0:
                            break;
                        case 1:
                            var users = wsClient.GetAllUsers();
                            foreach (User user in users)
                            {
                                Console.Write("ID: " + user.ID + "\t");
                                Console.Write("Imię: " + user.Name + "\t");
                                Console.Write("Wiek: " + user.Age + "\t");
                                Console.WriteLine("Email: " + user.Email + "\t");
                            }
                            break;
                        case 2:
                            Console.Write("Podaj imię: ");
                            name = Console.ReadLine();
                            Console.Write("Podaj wiek: ");
                            age = GetNumberFromUser();
                            Console.Write("Podaj email: ");
                            email = Console.ReadLine();
                            wsClient.AddUser(name, age, email);
                            Console.WriteLine("Dodano użytkownika");
                            break;
                        case 3:
                            Console.Write("Podaj ID: ");
                            id = GetNumberFromUser();
                            var foundUser = wsClient.GetUser(id);
                            Console.Write("ID: " + foundUser.ID + "\t");
                            Console.Write("Imię: " + foundUser.Name + "\t");
                            Console.Write("Wiek: " + foundUser.Age + "\t");
                            Console.WriteLine("Email: " + foundUser.Email + "\t");
                            break;
                        case 4:
                            Console.Write("Podaj id: ");
                            id = GetNumberFromUser();
                            Console.Write("Enter user name: ");
                            name = Console.ReadLine();
                            Console.Write("Podaj wiek: ");
                            age = GetNumberFromUser();
                            Console.Write("Podaj email: ");
                            email = Console.ReadLine();
                            wsClient.UpdateUser(id, name, age, email);
                            break;
                        case 5:
                            Console.Write("Podaj id: ");
                            id = GetNumberFromUser();
                            wsClient.DeleteUser(id);
                            break;
                        case 6:
                            Console.Write("Podaj imię po którym sortujesz: ");
                            name = Console.ReadLine();
                            var usersByName = await wsClient.FilterByAsync(name);
                            foreach (User user in usersByName)
                            {
                                Console.Write("ID: " + user.ID + "\t");
                                Console.Write("Imię: " + user.Name + "\t");
                                Console.Write("Wiek: " + user.Age + "\t");
                                Console.WriteLine("Email: " + user.Email + "\t");
                            }
                            break;
                        case 7:
                            Console.WriteLine($"Liczba użytkowników bazy: {wsClient.GetUserDatabaseSize()}");
                            break;
                        default:
                            Console.WriteLine("Niepoprawny input.");
                            break;
                    }
                }
                catch (FaultException ex)
                {
                    Console.WriteLine($"Błąd: {ex.Message}");
                }
                catch (TimeoutException tex)
                {
                    Console.WriteLine(tex.Message);
                }
                Console.WriteLine();
            } while (operation != 0);

            Console.WriteLine("Koniec działania programu");
            wsClient.Close();
        }

    }
}
