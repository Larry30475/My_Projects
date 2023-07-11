using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;

namespace GrpcGreeter.Information
{
    public class MyData
    {
        public static void info()
        {
            Console.WriteLine("Wiktor Sadowy 260373");
            Console.WriteLine("Vanya Luzhanski 247372");
            Console.WriteLine(DateTime.Now.ToString("dd.MMMM, HH:mm:ss"));
            Console.WriteLine(typeof(string).Assembly.ImageRuntimeVersion);
            Console.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Console.WriteLine(RuntimeInformation.OSDescription);
            Console.WriteLine(GetLocalIPAddress());
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
