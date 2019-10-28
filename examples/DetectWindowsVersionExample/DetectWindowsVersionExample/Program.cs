using System;

namespace DetectWindowsVersionDotNET
{
    class Program
    {
        static void Main(string[] args)
        {
            // The .NET Framework provides a class to find out the version of Windows (e.g. 6.2.9200)
            // However, this does not work as desired if the function is executed on Windows 10, Windows Server 2019, Windows Server 2016, Windows 8.1, Windows Server 2012 R2
            // This means for example: for Windows 10 it will return 6.2, which is wrong, as this refers to Windows 8 / Windows Server 2012.

            Console.WriteLine($"" +
                    $"{System.Environment.OSVersion.Version.Major}." +
                    $"{System.Environment.OSVersion.Version.Minor}." +
                    $"{System.Environment.OSVersion.Version.Build}" +
                    $"");

			// Wait for input
            Console.ReadKey();
        }
    }
}
