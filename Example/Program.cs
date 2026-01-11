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
