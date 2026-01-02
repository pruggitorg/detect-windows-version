using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSVersionExtension;
using System;

namespace OSVersionExtensionTests
{
    [TestClass]
    public class OSVersionTests
    {
        [TestMethod]
        public void EnsureSetWin32ApiProviderThrowsException()
        {
            Action setWin32ApiProvider = () => OSVersion.SetWin32ApiProvider(null);

            Assert.Throws<ArgumentNullException>(setWin32ApiProvider);
        }

        [TestMethod]
        public void EnsureSetEnvironmentProviderThrowsException()
        {
            Action setEnvironmentProvider = () => OSVersion.SetEnvironmentProvider(null);

            Assert.Throws<ArgumentNullException>(setEnvironmentProvider);
        }

    }
}
