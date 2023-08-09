using OSVersionExt;
using OSVersionExt.Win32API;
using System.Collections.Generic;

namespace OSVersionExtensionTests
{
    internal static class Windows11Rules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const int BUILDNUMBER = 22000;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    internal static class WindowsServer2022ServerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const int BUILDNUMBER = 20348;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2022DomainControllerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const int BUILDNUMBER = 20348;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class Windows10Rules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    internal static class WindowsServer2019ServerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const int BUILDNUMBER = 17763;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2019DomainControllerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const int BUILDNUMBER = 17763;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class WindowsServer2016ServerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2016DomainControllerRules
    {
        internal const int MAJORVERSION = 10;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class WindowsServer2012R2ServerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 3;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2012R29DomainControllerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 3;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class Windows81Rules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 3;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    internal static class Windows8Rules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    internal static class WindowsServer2012ServerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2012DomainControllerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class Windows7Rules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 1;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    internal static class WindowsServer2008R2ServerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 1;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2008R2DomainControllerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 1;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class WindowsServer2008ServerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
    }

    internal static class WindowsServer2008DomainControllerRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.DomainController;
    }

    internal static class WindowsVistaRules
    {
        internal const int MAJORVERSION = 6;
        internal const int MINORVERSION = 0;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
    }

    /// <summary>
    /// Windows Server 2003 R2 detection rules
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexa</remarks>
    internal static class WindowsServer2003R2Rules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
        internal static readonly List<KeyValuePair<SystemMetric, int>> SYSTEMMETRICS = new List<KeyValuePair<SystemMetric, int>>()
                                            { new KeyValuePair<SystemMetric, int>( SystemMetric.SM_SERVERR2, 1 ) };
    }

    /// <summary>
    /// Windows Server 2003 detection rules
    /// </summary>
    /// <remarks>https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexa</remarks>
    internal static class WindowsServer2003Rules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.Server;
        internal static readonly List<KeyValuePair<SystemMetric, int>> SYSTEMMETRICS = new List<KeyValuePair<SystemMetric, int>>()
                                            { new KeyValuePair<SystemMetric, int>( SystemMetric.SM_SERVERR2, 0 ) };
    }

    internal static class WindowsNomeServerRules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 2;
        internal const SuiteMask SUITEMASK = SuiteMask.VER_SUITE_WH_SERVER;
    }

    internal static class WindowsXPProx64Rules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 2;
        internal const ProductType PRODUCTTYPE = ProductType.Workstation;
        internal const bool IS64BITOPERATINGSYSTEM = true;
    }

    internal static class WindowsXPRules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 1;
    }

    internal static class Windows2000Rules
    {
        internal const int MAJORVERSION = 5;
        internal const int MINORVERSION = 0;
    }

}
