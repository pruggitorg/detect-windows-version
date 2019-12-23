using OSVersionExt;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OSVersionExtension
{
    [SecurityCritical]
    public static class OSVersion
    {
        /// <summary>
        /// Detects Windows version
        /// </summary>
        /// <remarks>
        /// References:
        /// OSVERSIONINFOEXA structure https://docs.microsoft.com/de-de/windows/win32/api/winnt/ns-winnt-osversioninfoexa
        /// OSVERSIONINFOEX uses incorrect charset with RtlGetVersion() https://github.com/windows-toolkit/WindowsCommunityToolkit/issues/2095
        /// taken from https://stackoverflow.com/a/49641055
        /// </remarks>
        [SecurityCritical]
        [DllImport("ntdll.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern NTSTATUS RtlGetVersion(ref OSVERSIONINFOEX versionInfo);
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        internal struct OSVERSIONINFOEX
        {
            // The OSVersionInfoSize field must be set to Marshal.SizeOf(typeof(OSVERSIONINFOEX))
            internal int OSVersionInfoSize;
            internal int MajorVersion;
            internal int MinorVersion;
            internal int BuildNumber;
            internal int PlatformId;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            internal string CSDVersion;
            internal ushort ServicePackMajor;
            internal ushort ServicePackMinor;
            internal SuiteMask SuiteMask;
            internal ProductType ProductType;
            internal byte Reserved;
        }

        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(SystemMetric smIndex);

        public static int MajorVersion { get; private set; }
        public static int MinorVersion { get; private set; }
        public static int BuildNumber { get; private set; }
        public static bool IsWorkstation { get => GetIfWorkStation(); }
        public static bool IsServer { get => GetIfServer(); }

        private static ProductType _productType;
        private static SuiteMask _suiteMask;

        [SecurityCritical]
        static OSVersion()
        {
            DetectWindowsVersion();
        }

        /// <summary>
        /// Detects Windows version starting with Windows 2000 and also works on Windows 10/Server 2019/Server 2016 right away.
        /// </summary>
        /// <remarks>Calls the Windows Kernel function RtlGetVersion routine. </remarks>
        /// <returns></returns>        
        public static VersionInfo GetOSVersion()
        {
            return new VersionInfo(MajorVersion, MinorVersion, BuildNumber);
        }

        private static void DetectWindowsVersion()
        {
            var osVersionInfo = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            if (RtlGetVersion(ref osVersionInfo) != NTSTATUS.STATUS_SUCCESS)
            {
                // TODO: Error handling, call GetVersionEx, etc.
            }

            MajorVersion = osVersionInfo.MajorVersion;
            MinorVersion = osVersionInfo.MinorVersion;
            BuildNumber = osVersionInfo.BuildNumber;

            _productType = osVersionInfo.ProductType;
            _suiteMask = osVersionInfo.SuiteMask;
        }

        private static bool GetIfServer()
        {
            return _productType == ProductType.DomainController
                   || _productType == ProductType.Server;
        }

        private static bool GetIfWorkStation()
        {
            return _productType == ProductType.Workstation;
        }

        /// <summary>
        /// The system metric or configuration setting to be retrieved. 
        /// Note that all SM_CX* values are widths and all SM_CY* values are heights. 
        /// Also note that all settings designed to return Boolean data represent TRUE as any nonzero value, and FALSE as a zero value.
        /// </summary>
        /// <param name="smIndex"></param>
        /// <returns></returns>
        /// <remarks>https://docs.microsoft.com/de-de/windows/win32/api/winuser/nf-winuser-getsystemmetrics</remarks>
        private static int ReadSystemMetrics(SystemMetric smIndex)
        {
            return GetSystemMetrics(smIndex);
        }

        /// <summary>
        /// Returns the Windows version.
        /// </summary>
        /// <returns></returns>
        /// <remarks>detection based on https://docs.microsoft.com/de-de/windows-hardware/drivers/ddi/wdm/ns-wdm-_osversioninfoexw#remarks </remarks>
        public static OperatingSystem GetOperatingSystem()
        {
            if (MajorVersion == 10 && MinorVersion == 0 && IsWorkstation)
                return OperatingSystem.Windows10;
            else if (MajorVersion == 10 && MinorVersion == 0 && IsServer)
                return OperatingSystem.WindowsServer20162019;
            else if (MajorVersion == 6 && MinorVersion == 3 && IsServer)
                return OperatingSystem.WindowsServer2012R2;
            else if (MajorVersion == 6 && MinorVersion == 3 && IsWorkstation)
                return OperatingSystem.Windows81;
            else if (MajorVersion == 6 && MinorVersion == 2 && IsWorkstation)
                return OperatingSystem.Windows8;
            else if (MajorVersion == 6 && MinorVersion == 2 && IsServer)
                return OperatingSystem.WindowsServer2012;
            else if (MajorVersion == 6 && MinorVersion == 1 && IsWorkstation)
                return OperatingSystem.Windows7;
            else if (MajorVersion == 6 && MinorVersion == 1 && IsServer)
                return OperatingSystem.WindowsServer2008R2;
            else if (MajorVersion == 6 && MinorVersion == 0 && IsServer)
                return OperatingSystem.WindowsServer2008;
            else if (MajorVersion == 6 && MinorVersion == 0 && IsWorkstation)
                return OperatingSystem.WindowsVista;
            else if (MajorVersion == 5 && MinorVersion == 2 && ReadSystemMetrics(SystemMetric.SM_SERVERR2) != 0)
                return OperatingSystem.WindowsServer2003R2;
            else if (MajorVersion == 5
                     && MinorVersion == 2
                     && ReadSystemMetrics(SystemMetric.SM_SERVERR2) == 0
                     && (_suiteMask & SuiteMask.VER_SUITE_WH_SERVER) != SuiteMask.VER_SUITE_WH_SERVER
                     )
                return OperatingSystem.WindowsServer2003;
            else if (MajorVersion == 5 && MinorVersion == 2 && (_suiteMask & SuiteMask.VER_SUITE_WH_SERVER) == SuiteMask.VER_SUITE_WH_SERVER)
                return OperatingSystem.WindowsHomeServer;
            else if (MajorVersion == 5 && MinorVersion == 2 && IsWorkstation && Environment.Is64BitOperatingSystem)
                return OperatingSystem.WindowsXPProx64;
            else if (MajorVersion == 5 && MinorVersion == 1)
                return OperatingSystem.WindowsXP;
            else if (MajorVersion == 5 && MinorVersion == 0)
                return OperatingSystem.Windows2000;

            return OperatingSystem.Unknown;
        }
    }

    public enum OperatingSystem
    {
        Unknown,
        Windows2000,
        WindowsXP,
        WindowsXPProx64,
        WindowsHomeServer,
        WindowsServer2003,
        WindowsServer2003R2,    // tested
        WindowsVista,
        WindowsServer2008,
        WindowsServer2008R2,
        Windows7,
        WindowsServer2012,
        Windows8,
        Windows81,
        WindowsServer2012R2,    // tested
        WindowsServer20162019,  // tested Server 2019
        Windows10               // tested
    }

    public enum ProductType : byte
    {
        /// <summary>
        /// The operating system is Windows 10, Windows 8, Windows 7,...
        /// </summary>
        /// <remarks>VER_NT_WORKSTATION</remarks>
        Workstation = 0x0000001,
        /// <summary>
        /// The system is a domain controller and the operating system is Windows Server.
        /// </summary>
        /// <remarks>VER_NT_DOMAIN_CONTROLLER</remarks>
        DomainController = 0x0000002,
        /// <summary>
        /// The operating system is Windows Server. Note that a server that is also a domain controller
        /// is reported as VER_NT_DOMAIN_CONTROLLER, not VER_NT_SERVER.
        /// </summary>
        /// <remarks>VER_NT_SERVER</remarks>
        Server = 0x0000003
    }


    [Flags]
    enum SuiteMask : ushort
    {
        /// <summary>
        /// Microsoft BackOffice components are installed. 
        /// </summary>
        VER_SUITE_BACKOFFICE = 0x00000004,
        /// <summary>
        /// Windows Server 2003, Web Edition is installed
        /// </summary>
        VER_SUITE_BLADE = 0x00000400,
        /// <summary>
        /// Windows Server 2003, Compute Cluster Edition is installed.
        /// </summary>
        VER_SUITE_COMPUTE_SERVER = 0x00004000,
        /// <summary>
        /// Windows Server 2008 Datacenter, Windows Server 2003, Datacenter Edition, or Windows 2000 Datacenter Server is installed. 
        /// </summary>
        VER_SUITE_DATACENTER = 0x00000080,
        /// <summary>
        /// Windows Server 2008 Enterprise, Windows Server 2003, Enterprise Edition, or Windows 2000 Advanced Server is installed.
        /// Refer to the Remarks section for more information about this bit flag. 
        /// </summary>
        VER_SUITE_ENTERPRISE = 0x00000002,
        /// <summary>
        /// Windows XP Embedded is installed. 
        /// </summary>
        VER_SUITE_EMBEDDEDNT = 0x00000040,
        /// <summary>
        /// Windows Vista Home Premium, Windows Vista Home Basic, or Windows XP Home Edition is installed. 
        /// </summary>
        VER_SUITE_PERSONAL = 0x00000200,
        /// <summary>
        /// Remote Desktop is supported, but only one interactive session is supported. This value is set unless the system is running in application server mode. 
        /// </summary>
        VER_SUITE_SINGLEUSERTS = 0x00000100,
        /// <summary>
        /// Microsoft Small Business Server was once installed on the system, but may have been upgraded to another version of Windows.
        /// Refer to the Remarks section for more information about this bit flag. 
        /// </summary>
        VER_SUITE_SMALLBUSINESS = 0x00000001,
        /// <summary>
        /// Microsoft Small Business Server is installed with the restrictive client license in force. Refer to the Remarks section for more information about this bit flag. 
        /// </summary>
        VER_SUITE_SMALLBUSINESS_RESTRICTED = 0x00000020,
        /// <summary>
        /// Windows Storage Server 2003 R2 or Windows Storage Server 2003is installed. 
        /// </summary>
        VER_SUITE_STORAGE_SERVER = 0x00002000,
        /// <summary>
        /// Terminal Services is installed. This value is always set.
        /// If VER_SUITE_TERMINAL is set but VER_SUITE_SINGLEUSERTS is not set, the system is running in application server mode.
        /// </summary>
        VER_SUITE_TERMINAL = 0x00000010,
        /// <summary>
        /// Windows Home Server is installed. 
        /// </summary>
        VER_SUITE_WH_SERVER = 0x00008000

        //VER_SUITE_MULTIUSERTS = 0x00020000
    }

    enum NTSTATUS : uint
    {
        /// <summary>
        /// The operation completed successfully. 
        /// </summary>
        STATUS_SUCCESS = 0x00000000
    }
}
