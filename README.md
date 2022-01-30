# Properly detect Windows version in C# .NET – even Windows 10
Allows you to determine the correct Windows version, since System.Environment.OSVersion.Version in .NET Framework and .NET Core until version 4.8 respectively 3.1 returns wrong results on Windows 10. It works starting with Windows 2000 and also on Windows 10/Server 2019/Server 2016 right away.

Also available on Nuget: https://www.nuget.org/packages/OSVersionExt/

<img src="images/windows10-version-demo.png">


<pre><code class='language-cs'>
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
</code></pre>



# List of detected operating systems

The class can return the OS as an enum. 

<pre><code class='language-cs'>
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
        WindowsServer20162019,  
        Windows10               
    }
</code></pre>

| Operating system  | tested | remarks |
| ------------- | ------------- | -------------  |
| Windows 10  | yes  | 21H2 (build 19044), 21H1 (build 19043), 2009/20H2 (build 19042), 2004 (build 19041), 1909 (build 18363), 1903 (build 18362), 1809 (build 17763) - all x64  |
| Windows Server 2019  |yes  |   |
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
NET Framework 4 (as one of the checks calls Environment.Is64BitOperatingSystem). Yet, you can inject your own environment provider, which runs on lower versions.

# Technical information
Please refer to https://www.prugg.at/2019/09/09/properly-detect-windows-version-in-c-net-even-windows-10/
