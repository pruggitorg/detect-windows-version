# Windows Version & Edition Detection Library for C# .NET

Determining the Windows version and edition can be challenging.

First, the `System.Environment.OSVersion.Version` property in the .NET Framework up to version 4.8.1 and in .NET Core up to version 3.1 returns incorrect results on Windows 10 and later versions. In addition, correctly identifying the Windows edition, such as distinguishing between Windows 10 and Windows 11, adds another layer of complexity, as it requires handling build numbers.

This library abstracts all of these challenges. It returns the detected Windows edition as a strongly typed enum, without the need to deal with ambiguous string representations.  
It works out of the box on Windows 11, Windows 10, Windows Server 2022, and Windows Server 2019, and also supports earlier Windows versions.

Also available on NuGet: https://www.nuget.org/packages/OSVersionExt/


<img src="images/windows11-version-demo.png">


```csharp
using OSVersionExtension;

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
    Console.WriteLine($"Windows Display Version: {OSVersion.MajorVersion10Properties().DisplayVersion ?? "(Unable to detect)"}");
    Console.WriteLine($"Windows Update Build Revision: {OSVersion.MajorVersion10Properties().UBR ?? "(Unable to detect)"}");
}

Console.ReadKey();

```



# List of detected operating systems

The class can return the OS as an enum. 

```csharp
    public enum OperatingSystem
    {
        Unknown,
        Windows2000,
        WindowsXP,
        WindowsXPProx64,
        WindowsHomeServer,
        WindowsServer2003,
        WindowsServer2003R2, 
        WindowsVista,
        WindowsServer2008,
        WindowsServer2008R2,
        Windows7,
        WindowsServer2012,
        Windows8,
        Windows81,
        WindowsServer2012R2,
        WindowsServer2016,
        WindowsServer2019,
        Windows10,
        Windows11,
        WindowsServer2022
    }
```

| Operating system  | tested | remarks |
| ------------- | ------------- | -------------  |
| Windows 11  | yes  |   |
| Windows Server 2022  | yes  |   |
| Windows 10  | yes  | 21H2 (build 19044), 21H1 (build 19043), 2009/20H2 (build 19042), 2004 (build 19041), 1909 (build 18363), 1903 (build 18362), 1809 (build 17763) - all x64  |
| Windows Server 2019  | yes  |   |
| Windows Server 2016  | yes  |   |
| Windows Server 2012 R2  | yes  |   |
| Windows 8.1  | yes  | x64  |
| Windows 8  | -  |   |
| Windows 7  | yes  | x86  |
| Windows Server 2008 R2  | yes  | x64  |
| Windows Server 2008  | -  |   |
| Windows Vista  | -  |   |
| Windows Server 2003 R2  | yes  |   |
| Windows Server 2003  | -  |   |
| Windows Home Server  | -  |   |
| Windows XP 64-Bit Edition  | -  |   |
| Windows XP  | -  |   |
| Windows 2000  | -  |   |


# Target framework
This library targets .NET Standard 2.0, ensuring broad cross-platform compatibility. By using .NET Standard, the library can be consumed by multiple .NET implementations, including:

* .NET Framework (version 4.6.1 and later)
* .NET Core (version 2.0 and later)
* .NET 5+

This makes the class usable across different environments without modification, providing maximum flexibility for developers building applications on various platforms.
For more details on .NET Standard and supported platforms, please refer to the official documentation:
https://learn.microsoft.com/en-us/dotnet/standard/net-standard#?tabs=net-standard-2-0
