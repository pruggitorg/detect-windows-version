using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OSVersionExtension;

namespace DetectWindowsVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            // Calling RtlGetVersion in ntdll.dll
            // The Windows Kernel offers an interesting function. The RtlGetVersion routine returns version information about the 
            // currently running operating system. It is available starting with Windows 2000 and also works on
            // Windows 10 / Server 2019 / Server 2016 right away.

            Console.WriteLine($"" +
            $"{OSVersion.GetOSVersion().Major}." +
            $"{OSVersion.GetOSVersion().Minor}." +
            $"{OSVersion.GetOSVersion().Build}" +
            $"");

            Console.ReadKey();
        }
    }
}
