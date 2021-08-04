using OSVersionExt;
using OSVersionExt.Environment;
using OSVersionExt.Win32API;
using OSVersionExt.MajorVersion10;
using System;
using System.Runtime.InteropServices;
using System.Security;

namespace OSVersionExtension
{
    /// <summary>
    /// Detects Windows version starting with Windows 2000 and also works on Windows 10/Server 2019/Server 2016 right away.
    /// </summary>
    /// <remarks>
    /// References:
    /// OSVERSIONINFOEXA structure https://docs.microsoft.com/de-de/windows/win32/api/winnt/ns-winnt-osversioninfoexa
    /// OSVERSIONINFOEX uses incorrect charset with RtlGetVersion() https://github.com/windows-toolkit/WindowsCommunityToolkit/issues/2095
    /// taken from https://stackoverflow.com/a/49641055
    /// </remarks>
    [SecurityCritical]
    public static class OSVersion
    {
        /// <summary>
        /// Default provider for the Win32 API.
        /// </summary>
        private static readonly IWin32API _win32ApiProviderDefault;

        /// <summary>
        /// Default provider for the Windows environment.
        /// </summary>
        private static readonly IEnvironment _environmentProviderDefault;

        /// <summary>
        /// Provider for working with the Win32 API.
        /// </summary>
        private static IWin32API _win32ApiProvider;

        /// <summary>
        /// Provider for getting information about the Windows environment.
        /// </summary>
        private static IEnvironment _environmentProvider;

        public static int MajorVersion { get => DetectWindowsVersion(_win32ApiProvider).MajorVersion; }
        public static int MinorVersion { get => DetectWindowsVersion(_win32ApiProvider).MinorVersion; }
        public static int BuildNumber { get => DetectWindowsVersion(_win32ApiProvider).BuildNumber; }
        public static bool IsWorkstation { get => GetIfWorkStation(); }
        public static bool IsServer { get => GetIfServer(); }
        public static bool Is64BitOperatingSystem { get => GetIf64BitOperatingSystem(); }

        [SecurityCritical]
        static OSVersion()
        {
            _win32ApiProviderDefault = new Win32ApiProvider();
            _environmentProviderDefault = new EnvironmentProvider();

            _win32ApiProvider = _win32ApiProviderDefault;
            _environmentProvider = _environmentProviderDefault;            
        }

        /// <summary>
        /// Gets the version information and build number.
        /// </summary>
        /// <remarks></remarks>
        /// <returns>Returns <see cref="VersionInfo"/>, e.g 10.0.19043</returns>        
        public static VersionInfo GetOSVersion()
        {
            return new VersionInfo(MajorVersion, MinorVersion, BuildNumber);
        }

        /// <summary>
        /// Returns the Windows version.
        /// </summary>
        /// <returns></returns>
        /// <remarks>detection based on https://docs.microsoft.com/de-de/windows-hardware/drivers/ddi/wdm/ns-wdm-_osversioninfoexw#remarks </remarks>
        public static OperatingSystem GetOperatingSystem()
        {
            SuiteMask suiteMask = DetectWindowsVersion(_win32ApiProvider).SuiteMask;

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
            else if (MajorVersion == 5 &&
                     MinorVersion == 2 &&
                     IsServer &&
                     ReadSystemMetrics(SystemMetric.SM_SERVERR2) != 0)
                return OperatingSystem.WindowsServer2003R2;
            else if (MajorVersion == 5 &&
                     MinorVersion == 2 &&
                     IsServer &&
                     ReadSystemMetrics(SystemMetric.SM_SERVERR2) == 0 &&
                     (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) != SuiteMask.VER_SUITE_WH_SERVER
                     )
                return OperatingSystem.WindowsServer2003;
            else if (MajorVersion == 5 && MinorVersion == 2 && (suiteMask & SuiteMask.VER_SUITE_WH_SERVER) == SuiteMask.VER_SUITE_WH_SERVER)
                return OperatingSystem.WindowsHomeServer;
            else if (MajorVersion == 5 && MinorVersion == 2 && IsWorkstation && Is64BitOperatingSystem)
                return OperatingSystem.WindowsXPProx64;
            else if (MajorVersion == 5 && MinorVersion == 1)
                return OperatingSystem.WindowsXP;
            else if (MajorVersion == 5 && MinorVersion == 0)
                return OperatingSystem.Windows2000;

            return OperatingSystem.Unknown;
        }

        /// <summary>
        /// Use custom Win32 API provider.
        /// </summary>
        /// <param name="win32ApiProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetWin32ApiProvider(IWin32API win32ApiProvider)
        {
            _ = win32ApiProvider ?? throw new ArgumentNullException();

            _win32ApiProvider = win32ApiProvider;            
        }

        /// <summary>
        /// Use custom Environment provider
        /// </summary>
        /// <param name="environmentProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void SetEnvironmentProvider(IEnvironment environmentProvider)
        {
            _ = environmentProvider ?? throw new ArgumentNullException();

            _environmentProvider = environmentProvider;            
        }

        /// <summary>
        /// Sets the Win32API Provider to default.
        /// </summary>
        public static void SetWin32ApiProviderDefault()
        {
            _win32ApiProvider = _win32ApiProviderDefault;
        }

        /// <summary>
        /// Sets the Environment provider to default.
        /// </summary>
        public static void SetEnvironmentProviderDefault()
        {
            _environmentProvider = _environmentProviderDefault;            
        }

        /// <summary>
        /// Returns additional properties for Windows systems having major version 10 or higher
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Cannot be called on systems other than Windows 10</exception>
        public static MajorVersion10Properties MajorVersion10Properties()
        {            
            if (MajorVersion < 10)
                throw new InvalidOperationException("Cannot be called on systems earlier than version 10.");

            MajorVersion10Properties majorVersion10Properties = new MajorVersion10Properties();

            return majorVersion10Properties;
        }

        private static OSVERSIONINFOEX DetectWindowsVersion(IWin32API win32ApiProvider)
        {
            var osVersionInfo = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            if (win32ApiProvider.RtlGetVersion(ref osVersionInfo) != NTSTATUS.STATUS_SUCCESS)
                throw new InvalidOperationException($"Failed to call internal {nameof(win32ApiProvider.RtlGetVersion)}.");

            return osVersionInfo;
        }

        private static bool GetIfServer()
        {
            ProductType productType = DetectWindowsVersion(_win32ApiProvider).ProductType;

            return productType == ProductType.DomainController
                   || productType == ProductType.Server;
        }

        private static bool GetIfWorkStation()
        {
            ProductType productType = DetectWindowsVersion(_win32ApiProvider).ProductType;

            return productType == ProductType.Workstation;
        }

        private static bool GetIf64BitOperatingSystem()
        {
            return _environmentProvider.Is64BitOperatingSystem();
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
            return _win32ApiProvider.GetSystemMetrics(smIndex);
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

}
