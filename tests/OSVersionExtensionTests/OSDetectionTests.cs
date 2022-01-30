using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSVersionExt;
using OSVersionExt.Win32API;
using OSVersionExtension;
using OSVersionExtensionTests.Mocks;

namespace OSVersionExtensionTests
{
    [TestClass]
    public class OSDetectionTests
    {
        [DataTestMethod]
        [DataRow(Windows10Rules.MAJORVERSION, Windows10Rules.MINORVERSION, Windows10Rules.PRODUCTTYPE)]
        public void DetectWindows10(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Windows10, operatingSystem);
        }


        [DataTestMethod]
        [DataRow(WindowsServer20162019ServerRules.MAJORVERSION, WindowsServer20162019ServerRules.MINORVERSION,
            WindowsServer20162019ServerRules.PRODUCTTYPE)]
        [DataRow(WindowsServer20162019DomainControllerRules.MAJORVERSION, WindowsServer20162019DomainControllerRules.MINORVERSION,
            WindowsServer20162019DomainControllerRules.PRODUCTTYPE)]
        public void DetectWindowsServer20162019(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer20162019, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsServer2012R2ServerRules.MAJORVERSION, WindowsServer2012R2ServerRules.MINORVERSION,
            WindowsServer2012R2ServerRules.PRODUCTTYPE)]
        [DataRow(WindowsServer2012R29DomainControllerRules.MAJORVERSION, WindowsServer2012R29DomainControllerRules.MINORVERSION,
            WindowsServer2012R29DomainControllerRules.PRODUCTTYPE)]
        public void DetectWindowsServer2012R2(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2012R2, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(Windows81Rules.MAJORVERSION, Windows81Rules.MINORVERSION, Windows81Rules.PRODUCTTYPE)]
        public void DetectWindows81(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Windows81, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(Windows8Rules.MAJORVERSION, Windows8Rules.MINORVERSION, Windows8Rules.PRODUCTTYPE)]
        public void DetectWindows8(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Windows8, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsServer2012ServerRules.MAJORVERSION, WindowsServer2012ServerRules.MINORVERSION,
            WindowsServer2012ServerRules.PRODUCTTYPE)]
        [DataRow(WindowsServer2012DomainControllerRules.MAJORVERSION, WindowsServer2012DomainControllerRules.MINORVERSION,
            WindowsServer2012DomainControllerRules.PRODUCTTYPE)]
        public void DetectWindowsServer2012(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2012, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(Windows7Rules.MAJORVERSION, Windows7Rules.MINORVERSION, Windows7Rules.PRODUCTTYPE)]
        public void DetectWindows7(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Windows7, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsServer2008R2ServerRules.MAJORVERSION, WindowsServer2008R2ServerRules.MINORVERSION,
            WindowsServer2008R2ServerRules.PRODUCTTYPE)]
        [DataRow(WindowsServer2008R2DomainControllerRules.MAJORVERSION, WindowsServer2008R2DomainControllerRules.MINORVERSION,
            WindowsServer2008R2DomainControllerRules.PRODUCTTYPE)]
        public void DetectWindowsServer2008R2(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2008R2, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsServer2008ServerRules.MAJORVERSION, WindowsServer2008ServerRules.MINORVERSION,
            WindowsServer2008ServerRules.PRODUCTTYPE)]
        [DataRow(WindowsServer2008DomainControllerRules.MAJORVERSION, WindowsServer2008DomainControllerRules.MINORVERSION,
            WindowsServer2008DomainControllerRules.PRODUCTTYPE)]
        public void DetectWindowsServer2008(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2008, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsVistaRules.MAJORVERSION, WindowsVistaRules.MINORVERSION, WindowsVistaRules.PRODUCTTYPE)]
        public void DetectWindowsVista(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsVista, operatingSystem);
        }

        /// <summary>
        /// Check if Windows Server 2003 R2 is properly detected. According to Microsoft documentation, ProductType does not apply for this version,
        /// but SM_SERVERR2 != 0
        /// </summary>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        /// <param name="productType"></param>
        /// <remarks>https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexa</remarks>
        [DataTestMethod]
        [DataRow(WindowsServer2003R2Rules.MAJORVERSION, WindowsServer2003R2Rules.MINORVERSION, WindowsServer2003R2Rules.PRODUCTTYPE)]
        public void DetectWindowsServer2003R2(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;
            List<KeyValuePair<SystemMetric, int>> systemMetrics = WindowsServer2003R2Rules.SYSTEMMETRICS;


            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock, systemMetrics);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2003R2, operatingSystem);
        }

        /// <summary>
        /// Check if Windows Server 2003 is properly detected. According to Microsoft documentation, ProductType does not apply for this version,
        /// but SM_SERVERR2 == 0
        /// </summary>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        /// <param name="productType"></param>
        /// <remarks>https://docs.microsoft.com/en-us/windows/win32/api/winnt/ns-winnt-osversioninfoexa</remarks>
        [DataTestMethod]
        [DataRow(WindowsServer2003Rules.MAJORVERSION, WindowsServer2003Rules.MINORVERSION, WindowsServer2003Rules.PRODUCTTYPE)]
        public void DetectWindowsServer2003(int majorVersion, int minorVersion, ProductType productType)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;
            List<KeyValuePair<SystemMetric, int>> systemMetrics = WindowsServer2003Rules.SYSTEMMETRICS;


            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock, systemMetrics);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsServer2003, operatingSystem);
        }


        [DataTestMethod]
        [DataRow(WindowsNomeServerRules.MAJORVERSION, WindowsNomeServerRules.MINORVERSION, WindowsNomeServerRules.SUITEMASK)]
        public void DetectWindowsHomeServer(int majorVersion, int minorVersion, SuiteMask suiteMask)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.SuiteMask = suiteMask;


            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsHomeServer, operatingSystem);
        }

        /// <summary>
        /// Windows XP Pro 64 Bit
        /// </summary>
        /// <param name="majorVersion"></param>
        /// <param name="minorVersion"></param>
        /// <param name="productType"></param>        
        [DataTestMethod]
        [DataRow(WindowsXPProx64Rules.MAJORVERSION, WindowsXPProx64Rules.MINORVERSION, WindowsXPProx64Rules.PRODUCTTYPE, WindowsXPProx64Rules.IS64BITOPERATINGSYSTEM)]
        public void DetectWindowsXPProx64(int majorVersion, int minorVersion, ProductType productType, bool is64BitOperatingSystem)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;
            osVersionInfoMock.ProductType = productType;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            EnvironmentProviderMock environmentProviderMock = new EnvironmentProviderMock(is64BitOperatingSystem);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);
            OSVersion.SetEnvironmentProvider(environmentProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsXPProx64, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(WindowsXPRules.MAJORVERSION, WindowsXPRules.MINORVERSION)]
        public void DetectWindowsXP(int majorVersion, int minorVersion)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.WindowsXP, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(Windows2000Rules.MAJORVERSION, Windows2000Rules.MINORVERSION)]
        public void DetectWindows2000(int majorVersion, int minorVersion)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Windows2000, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(112233,0)]
        [DataRow(0, 112233)]
        public void UnknownWindowsOnUnknownVersionInformation(int majorVersion, int minorVersion)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Unknown, operatingSystem);
        }

        [DataTestMethod]
        [DataRow(0, 0)]        
        public void UnknownWindowsWhenVersionHasZeroValues(int majorVersion, int minorVersion)
        {
            // arrange            
            var osVersionInfoMock = new OSVERSIONINFOEX { OSVersionInfoSize = Marshal.SizeOf(typeof(OSVERSIONINFOEX)) };

            osVersionInfoMock.MajorVersion = majorVersion;
            osVersionInfoMock.MinorVersion = minorVersion;

            Win32ApiProviderMock win32ApiProviderMock = new Win32ApiProviderMock(osVersionInfoMock);
            OSVersion.SetWin32ApiProvider(win32ApiProviderMock);

            // act
            OSVersionExtension.OperatingSystem operatingSystem = OSVersion.GetOperatingSystem();

            // assert
            Assert.AreEqual(OSVersionExtension.OperatingSystem.Unknown, operatingSystem);
        }

    }
}
