using OSVersionExt;
using OSVersionExt.Win32API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSVersionExtensionTests.Mocks
{
    /// <summary>
    /// Mock for Win32 API environment.
    /// </summary>
    public class Win32ApiProviderMock : IWin32API
    {
        private OSVERSIONINFOEX _versionInfo;
        private List<KeyValuePair<SystemMetric, int>> _systemMetrics;

        public NTSTATUS RtlGetVersion(ref OSVERSIONINFOEX versionInfo)
        {
            versionInfo = _versionInfo;
            return NTSTATUS.STATUS_SUCCESS;
        }

        /// <summary>
        /// If the function succeeds, the return value is the requested system metric or configuration setting.
        /// If the function fails, the return value is 0
        /// </summary>
        /// <param name="smIndex"></param>
        /// <returns></returns>
        public int GetSystemMetrics(SystemMetric smIndex)
        {
            if (_systemMetrics != null)
                return _systemMetrics.Find(x => x.Key == smIndex).Value;
            return 0;
        }

        /// <summary>
        /// Creates a new instance of the Win32 API mock.
        /// </summary>
        /// <param name="versionInfo">Provide the desired versino information which should be returned by RtlGetVersion</param>
        /// <param name="systemMetrics">Provide the system metrics which should be returned by the Win32 API mock GetSystemMetrics.</param>
        public Win32ApiProviderMock(OSVERSIONINFOEX versionInfo, List<KeyValuePair<SystemMetric, int>> systemMetrics)
        {
            _versionInfo = versionInfo;
            _systemMetrics = systemMetrics;
        }

        /// <summary>
        /// Creates a new instance of the Win32 API mock.
        /// </summary>
        /// <param name="versionInfo">Provide the desired versino information which should be returned by RtlGetVersion</param>        

        public Win32ApiProviderMock(OSVERSIONINFOEX versionInfo)
        {
            _versionInfo = versionInfo;
        }

    }

}
