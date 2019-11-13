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

        /// <summary>
        /// Detects Windows version starting with Windows 2000 and also works on Windows 10/Server 2019/Server 2016 right away.
        /// </summary>
        /// <remarks>Calls the Windows Kernel function RtlGetVersion routine. </remarks>
        /// <returns></returns>
        [SecurityCritical]
        public static VersionInfo GetOSVersion()
            {
            var osVersionInfo = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };
            
            if (!RtlGetVersion(ref osVersionInfo))
            {
                // TODO: Error handling, call GetVersionEx, etc.
            }

            return new VersionInfo(osVersionInfo.MajorVersion, osVersionInfo.MinorVersion, osVersionInfo.BuildNumber);
        }
}
}
