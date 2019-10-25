using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectWindowsVersionDotNETappManifest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Targeting your application for Windows in the app.manifest:
            // In Visual Studio you can add an app manifest by doing the following:
            // Go to your project, right click and choose Add / New Item and choose Application Manifest File.
            // A new file will be added to your project having default name app.manifest. In the 
            // section <compatibility xmlns="urn:schemas-microsoft-com:compatibility.v1"> you can uncomment Windows 8.1 and 10.
            // If you run the program again, the output on a Windows 10 will be correct.

            Console.WriteLine($"" +
            $"{System.Environment.OSVersion.Version.Major}." +
            $"{System.Environment.OSVersion.Version.Minor}." +
            $"{System.Environment.OSVersion.Version.Build}" +
            $"");

            Console.ReadKey();
        }
    }
}
