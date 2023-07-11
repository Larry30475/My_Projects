using Client.Information;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace Client
{
    class Program
    {
        //xml add new      <Person xmlns="http://schemas.datacontract.org/2004/07/MyWebService"> <Name>Wiktor</Name> <Age>21</Age> <Email>Wiktor@g.com</Email> </Person>
        //json add new     { "Name": "Wiktor", "Age": 21, "Email": "wiktor@example.com" }

        //xml update       <Person xmlns="http://schemas.datacontract.org/2004/07/MyWebService"> <Id>2</Id> <Name>Wiktor</Name> <Age>21</Age> <Email>Wiktor@g.com</Email> </Person>
        //json update      { "Id": 1, "Name": "Wiktor", "Age": 21, "Email": "wiktor@example.com" }

        public static string URI = "http://172.20.10.10/Service1.svc";

        public static int GetNumberFromUser()
        {
            int number;
            if (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.WriteLine("Invalid input.");
                return GetNumberFromUser();
            }
            return number;
        }

        public static void addUser(string format)
        {
            Console.WriteLine("Podaj imię");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj wiek");
            int age = GetNumberFromUser();
            Console.WriteLine("Podaj email");
            string email = Console.ReadLine();

            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons");

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "POST";
            string data = "";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
                data = $"<Person xmlns=\"http://schemas.datacontract.org/2004/07/MyWebService\"><Name>{name}</Name><Age>{age}</Age><Email>{email}</Email></Person>";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
                data = "{" + $"\"Name\": \"{name}\", \"Age\": {age}, \"Email\": \"{email}\"" + "}";
            }

            byte[] bufor = Encoding.UTF8.GetBytes(data);
            req.ContentLength = bufor.Length;
            Stream postData = req.GetRequestStream();
            postData.Write(bufor, 0, bufor.Length);
            postData.Close();

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void printUser(string format)
        {
            Console.WriteLine("Podaj id");
            string id = Console.ReadLine();

            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons", id);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "GET";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
            }

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void updateUser(string format)
        {
            Console.WriteLine("Podaj id");
            string id = Console.ReadLine();
            Console.WriteLine("Podaj imię");
            string name = Console.ReadLine();
            Console.WriteLine("Podaj wiek");
            int age = GetNumberFromUser();
            Console.WriteLine("Podaj email");
            string email = Console.ReadLine();

            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons", id);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "PUT";
            string data = "";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
                data = $"<Person xmlns=\"http://schemas.datacontract.org/2004/07/MyWebService\"><Id>{id}</Id><Name>{name}</Name><Age>{age}</Age><Email>{email}</Email></Person>";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
                data = "{" + $"\"Id\": {id}, \"Name\": \"{name}\", \"Age\": {age}, \"Email\": \"{email}\"" + "}";
            }

            byte[] bufor = Encoding.UTF8.GetBytes(data);
            req.ContentLength = bufor.Length;
            Stream postData = req.GetRequestStream();
            postData.Write(bufor, 0, bufor.Length);
            postData.Close();

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void deleteUser(string format)
        {
            Console.WriteLine("Podaj id");
            string id = Console.ReadLine();

            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons", id);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "DELETE";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
            }

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void printAllUsers(string format)
        {
            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons");

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "GET";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
            }

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void printUserDatabaseSize(string format)
        {
            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons", "size");

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "GET";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
            }

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        public static void printAllUsersFilter(string format)
        {
            Console.WriteLine("Podaj imię");
            string name = Console.ReadLine();

            var uri = URI;
            if (format == "json")
            {
                uri = Path.Combine(uri, "json");
            }
            uri = Path.Combine(uri, "persons", "filter", name);

            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = "GET";
            if (format == "xml")
            {
                req.ContentType = "text/xml";
            }
            else if (format == "json")
            {
                req.ContentType = "application/json";
            }

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = Encoding.GetEncoding(1252);
            StreamReader responseStream = new StreamReader(resp.GetResponseStream(), enc);
            string responseString = responseStream.ReadToEnd();
            resp.Close();

            Console.WriteLine();
            Console.WriteLine("Odpowiedź z serwera");
            Console.WriteLine(responseString);
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            MyData.info();
            Console.WriteLine();

            while (true)
            {
                try
                {
                    int operation = -1;
                    Console.WriteLine("Wybierz operację:");
                    Console.WriteLine("1. Dodaj użytkownika");
                    Console.WriteLine("2. Zdobądź dane użytkownika");
                    Console.WriteLine("3. Aktualizuj użytkownika");
                    Console.WriteLine("4. Skasuj użytkownika");
                    Console.WriteLine("5. Zdobądź wszystkich użytkowników");
                    Console.WriteLine("6. Zdobądź rozmiar bazy");
                    Console.WriteLine("7. Zdobądź użytkowników o danym imieniu");
                    Console.WriteLine("0. Zakończ");

                    if (!int.TryParse(Console.ReadLine(), out operation))
                    {
                        Console.WriteLine("Niepoprawny input.\n");
                        operation = -1;
                        continue;
                    }
                    Console.WriteLine();
                    if (operation == 0)
                    {
                        break;
                    }

                    Console.Write("Podaj używany format (xml lub json): ");
                    string format = Console.ReadLine();

                    if (format != "xml" && format != "json")
                    {
                        Console.WriteLine("Niepoprawny input.\n");
                        operation = -1;
                        continue;
                    }

                    switch (operation)
                    {
                        case 1:
                            addUser(format);
                            break;
                        case 2:
                            printUser(format);
                            break;
                        case 3:
                            updateUser(format);
                            break;
                        case 4:
                            deleteUser(format);
                            break;
                        case 5:
                            printAllUsers(format);
                            break;
                        case 6:
                            printUserDatabaseSize(format);
                            break;
                        case 7:
                            printAllUsersFilter(format);
                            break;
                        default:
                            Console.WriteLine("Niepoprawny input.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    Console.WriteLine();
                }
            }

            Console.WriteLine("Żegnam");
        }
    }
}
