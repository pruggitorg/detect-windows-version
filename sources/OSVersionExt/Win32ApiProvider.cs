using OSVersionExt.Win32API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace OSVersionExt
{
    public class Win32ApiProvider : IWin32API
    {
        [SecurityCritical]
        [DllImport("ntdll.dll", EntryPoint = "RtlGetVersion", SetLastError = true, CharSet = CharSet.Unicode)]
        internal static extern NTSTATUS ntdll_RtlGetVersion(ref OSVERSIONINFOEX versionInfo);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        internal static extern int ntdll_GetSystemMetrics(SystemMetric smIndex);


        public NTSTATUS RtlGetVersion(ref OSVERSIONINFOEX versionInfo)
        {
            return ntdll_RtlGetVersion(ref versionInfo);
        }

        public int GetSystemMetrics(SystemMetric smIndex)
        {
            return ntdll_GetSystemMetrics(smIndex);
        }
    }
}
