using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace MyWebService.Information
{
    public class MyData
    {
        public static void info()
        {
            Debug.WriteLine("Wiktor Sadowy 260373");
            Debug.WriteLine("Ivan Luzhanskyi 247372");
            Debug.WriteLine(DateTime.Now.ToString("dd.MMMM, HH:mm:ss"));
            Debug.WriteLine(typeof(string).Assembly.ImageRuntimeVersion);
            Debug.WriteLine(System.Security.Principal.WindowsIdentity.GetCurrent().Name);
            Debug.WriteLine(RuntimeInformation.OSDescription);
            Debug.WriteLine(GetLocalIPAddress());
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