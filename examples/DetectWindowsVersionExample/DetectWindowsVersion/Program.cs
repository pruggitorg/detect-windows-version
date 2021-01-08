using System;
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

            Console.WriteLine($"Windows version: " +
            $"{OSVersion.GetOSVersion().Version.Major}." +
            $"{OSVersion.GetOSVersion().Version.Minor}." +
            $"{OSVersion.GetOSVersion().Version.Build}" +
            $"");

            Console.WriteLine($"OS type: {OSVersion.GetOperatingSystem()}");
            Console.WriteLine($"is workstation: {OSVersion.IsWorkstation}");
            Console.WriteLine($"is server: {OSVersion.IsServer}");
            Console.WriteLine($"64-Bit OS: {OSVersion.Is64BitOperatingSystem}");

            if (OSVersion.GetOSVersion().Version.Major >= 10)
            {
                Console.WriteLine($"Windows Release ID: {OSVersion.MajorVersion10Properties().ReleaseId ?? "(Unable to detect)"}");
                Console.WriteLine($"Windows Display Version: {OSVersion.MajorVersion10Properties().DisplayVersion ?? "(Unable to detect)"}");
                Console.WriteLine($"Windows Update Build Revision: {OSVersion.MajorVersion10Properties().UBR ?? "(Unable to detect)"}");
            }

            Console.ReadKey();
        }
    }
}
