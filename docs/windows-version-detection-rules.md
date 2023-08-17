# Windows detection rules

The table below shows the criteria used to determine the individual Windows editions. 

$~$

## Getting the Windows edition

| Windows Edition  | enum OperatingSystem  | Detection Rule |
| -------------         | -------------         | ------------- |
| Windows 11            | Windows11             | MajorVersion == 10 && MinorVersion == 0 && BuildNumber >= 22000 && IsWorkstation
| Windows 10            | Windows10             | MajorVersion == 10 && MinorVersion == 0 && IsWorkstation
| Windows Server 2022   | WindowsServer2022		| MajorVersion == 10 && MinorVersion == 0 && BuildNumber >= 20348 && IsServer
| Windows Server 2019   | WindowsServer2019		| MajorVersion == 10 && MinorVersion == 0 && BuildNumber >= 17763 && IsServer
| Windows Server 2016   | WindowsServer2016		| MajorVersion == 10 && MinorVersion == 0 && IsServer
| Windows Server 2012 R2| WindowsServer2012R2   | MajorVersion == 6 && MinorVersion == 3 && IsServer
| Windows 8.1           | Windows81             | MajorVersion == 6 && MinorVersion == 3 && IsWorkstation
| Windows 8             | Windows8              | MajorVersion == 6 && MinorVersion == 2 && IsWorkstation
| Windows Server 2012   | WindowsServer2012     | MajorVersion == 6 && MinorVersion == 2 && IsServer
| Windows 7             | Windows7              | MajorVersion == 6 && MinorVersion == 1 && IsWorkstation
| Windows Server 2008 R2| WindowsServer2008R2   | MajorVersion == 6 && MinorVersion == 1 && IsServer
| Windows Server 2008   | WindowsServer2008     | MajorVersion == 6 && MinorVersion == 0 && IsServer
| Windows Vista         | WindowsVista          | MajorVersion == 6 && MinorVersion == 0 && IsWorkstation
| Windows Server 2003 R2| WindowsServer2003R2   | MajorVersion == 5 && MinorVersion == 2 && IsServer && SystemMetric.SM_SERVERR2 != 0
| Windows Server 2003   | WindowsServer2003     | MajorVersion == 5 && MinorVersion == 2 && IsServer && SystemMetric.SM_SERVERR2 = 0 && (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) != SuiteMask.VER_SUITE_WH_SERVER
| Windows Home Server   | WindowsHomeServer     | MajorVersion == 5 && MinorVersion == 2 && (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) == SuiteMask.VER_SUITE_WH_SERVER
| Windows XP 64-Bit     | WindowsXPProx64       | MajorVersion == 5 && MinorVersion == 2 && IsWorkstation && Is64BitOperatingSystem
| Windows XP            | WindowsXP             | MajorVersion == 5 && MinorVersion == 1
| Windows 2000          | Windows2000           | MajorVersion == 5 && MinorVersion == 0


