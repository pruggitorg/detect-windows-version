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
        /// taken from https://stackoverflow.com/a/49641055
        /// </summary>
        /// <remarks>
        /// References:
        /// OSVERSIONINFOEXA structure https://docs.microsoft.com/de-de/windows/win32/api/winnt/ns-winnt-osversioninfoexa
        /// OSVERSIONINFOEX uses incorrect charset with RtlGetVersion() https://github.com/windows-toolkit/WindowsCommunityToolkit/issues/2095
        /// </remarks>
        [SecurityCritical]
        [DllImport("ntdll.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern bool RtlGetVersion(ref OSVERSIONINFOEX versionInfo);
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
            internal short SuiteMask;
            internal byte ProductType;
            internal byte Reserved;
        }

        public static int MajorVersion { get; private set; }
        public static int MinorVersion { get; private set; }
        public static int BuildNumber { get; private set; }        
        public static bool IsWorkstation { get => GetIfWorkStation(); }
        public static bool IsServer { get => GetIfServer(); }

        private static byte _productType;

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

            if (!RtlGetVersion(ref osVersionInfo))
            {
                // TODO: Error handling, call GetVersionEx, etc.
            }

            MajorVersion = osVersionInfo.MajorVersion;
            MinorVersion = osVersionInfo.MinorVersion;
            BuildNumber = osVersionInfo.BuildNumber;            

            _productType = osVersionInfo.ProductType;
        }

        private static bool GetIfServer()
        {
            return _productType == (byte)ProductType.DomainController
                   || _productType == (byte)ProductType.Server;
        }

        private static bool GetIfWorkStation()
        {
            return _productType == (byte)ProductType.Workstation;
        }

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
                return OperatingSystem.WindowsServer2008;

            return OperatingSystem.Unknown;
        }
    }

    public enum OperatingSystem
    {
        Unknown,
        //Windows2000,
        //WindowsXP,
        //WindowsXPProx64,
        //WindowsServer2003,
        //WindowsHomeServer,
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

    public enum ProductType
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
}
