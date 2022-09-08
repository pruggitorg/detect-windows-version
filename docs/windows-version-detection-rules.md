# Windows detection rules

Currently, the method officially supported by Microsoft is used to determine the Windows version. However, this means that no distinction can be made between Windows 10 and Windows 11, for example.

Presumably, the version number (e.g. 10.0) is to be understood as a kind of platform version. Related to Windows 10 and Windows 11 this means: it is technically the same system.
This can be interesting for developers for drivers, although Microsoft advises against relying on version number or build number. Instead, the driver manufacturer should check whether a desired Windows function is present or not.

The current implementation takes this into account, but has a design flaw in the enumeration. The product name is listed there (e.g. WindowsServer20162019), which led to awkward situations when Windows 2022 Server was released. As a new suggestion, there should be a new enumeration WindowsPlatform, which specifies, for example, WindowsServer2016OrHigher. Because that is the only clear statement that can be made.

Regardless, developers still want to determine whether it is Windows Server 2019 or 2022, for example. This will be one of the future developments of this project.

$~$

## Getting the Windows platform version

| Windows Product Name  | enum OperatingSystem (currently implemented, but will be obsolet)  | enum WindowsPlatform (new proposal)| Detection Rule |
| -------------         | -------------         | -------------                 | ------------- |
| Windows 11            | Windows10             | Windows10OrHigher             | MajorVersion == 10 && MinorVersion == 0 && IsWorkstation
| Windows 10            | Windows10             | Windows10OrHigher             | see above
| Windows Server 2022   | WindowsServer20162019 | WindowsServer2016OrHigher     | MajorVersion == 10 && MinorVersion == 0 && IsServer
| Windows Server 2019   | WindowsServer20162019 | WindowsServer2016OrHigher     | see above
| Windows Server 2016   | WindowsServer20162019 | WindowsServer2016OrHigher     | see above
| Windows Server 2012 R2| WindowsServer2012R2   | WindowsServer2012R2           | MajorVersion == 6 && MinorVersion == 3 && IsServer
| Windows 8.1           | Windows81             | Windows81                     | MajorVersion == 6 && MinorVersion == 3 && IsWorkstation
| Windows 8             | Windows8              | Windows8                      | MajorVersion == 6 && MinorVersion == 2 && IsWorkstation
| Windows Server 2012   | WindowsServer2012     | WindowsServer2012             | MajorVersion == 6 && MinorVersion == 2 && IsServer
| Windows 7             | Windows7              | Windows7                      | MajorVersion == 6 && MinorVersion == 1 && IsWorkstation
| Windows Server 2008 R2| WindowsServer2008R2   | WindowsServer2008R2           | MajorVersion == 6 && MinorVersion == 1 && IsServer
| Windows Server 2008   | WindowsServer2008     | WindowsServer2008             | MajorVersion == 6 && MinorVersion == 0 && IsServer
| Windows Vista         | WindowsVista          | WindowsVista                  | MajorVersion == 6 && MinorVersion == 0 && IsWorkstation
| Windows Server 2003 R2| WindowsServer2003R2   | WindowsServer2003R2           | MajorVersion == 5 && MinorVersion == 2 && IsServer && SystemMetric.SM_SERVERR2 != 0
| Windows Server 2003   | WindowsServer2003     | WindowsServer2003             | MajorVersion == 5 && MinorVersion == 2 && IsServer && SystemMetric.SM_SERVERR2 = 0 && (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) != SuiteMask.VER_SUITE_WH_SERVER
| Windows Home Server   | WindowsHomeServer     | WindowsHomeServer             | MajorVersion == 5 && MinorVersion == 2 && (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) == SuiteMask.VER_SUITE_WH_SERVER
| Windows XP 64-Bit     | WindowsXPProx64       | WindowsXPProx64               | MajorVersion == 5 && MinorVersion == 2 && IsWorkstation && Is64BitOperatingSystem
| Windows XP            | WindowsXP             | WindowsXP                     | MajorVersion == 5 && MinorVersion == 1
| Windows 2000          | Windows2000           | Windows2000                   | MajorVersion == 5 && MinorVersion == 0


