# Properly detect Windows version in C# .NET – even Windows 10
Shows various ways to determine the Windows version including calling RtlGetVersion in ntdll.dll. It is available starting with Windows 2000 and also works on Windows 10/Server 2019/Server 2016 right away.

Target framework: .NET Framework 4 (as one of the checks calls Environment.Is64BitOperatingSystem)


| Operating system  | tested | remarks |
| ------------- | ------------- | -------------  |
| Windows 10  | yes  | 1909 (build 18363), 1903 (build 18362), 1809 (build 17763)  |
| Windows Server 2019  |yes  |   |
| Windows Server 2016  | -  |   |
| Windows Server 2012 R2  | yes  |   |
| Windows 8.1  | -  |   |
| Windows 8  | -  |   |
| Windows 7  | -  |   |
| Windows Server 2008 R2  | -  |   |
| Windows Server 2008  | -  |   |
| Windows Vista  | -  |   |
| Windows Server 2003 R2  | yes  |   |
| Windows Server 2003  | -  |   |
| Windows Home Server  | -  |   |
| Windows XP 64-Bit Edition  | -  |   |
| Windows XP  | -  |   |
| Windows 2000  | -  |   |


# Technical information
Please refer to https://www.prugg.at/2019/09/09/properly-detect-windows-version-in-c-net-even-windows-10/
