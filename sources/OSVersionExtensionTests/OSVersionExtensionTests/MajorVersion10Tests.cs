using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OSVersionExtensionTests.Mocks;
using OSVersionExt.MajorVersion10;

namespace OSVersionExtensionTests
{
    /// <summary>
    /// Summary description for MajorVersion10Tests
    /// </summary>
    [TestClass]
    public class MajorVersion10Tests
    {

        private const string registryCurrentVersionKeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private const string releaseIdKeyName = "ReleaseId";
        private const string UBRkeyName = "UBR";

        public MajorVersion10Tests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EnsureBasicReadingInRegistry()
        {
            // arrange
            string expectedReleaseId = "1909";
            string expectedUbr = "1234";

            List<RegistryKeyNameReturnValue> registryKeyNameReturnValues = new List<RegistryKeyNameReturnValue>();
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() {KeyName = registryCurrentVersionKeyName, ValueName = releaseIdKeyName, ReturnValue = expectedReleaseId });
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() { KeyName = registryCurrentVersionKeyName, ValueName = UBRkeyName, ReturnValue = expectedUbr });

            RegistryProviderMock registryProviderMock = new RegistryProviderMock(registryKeyNameReturnValues);
            MajorVersion10Properties majorVersion10Properties = new MajorVersion10Properties(registryProviderMock);

            // act
            string releaseId = majorVersion10Properties.ReleaseId;
            string ubr = majorVersion10Properties.UBR;

            // assert
            Assert.AreEqual(expectedReleaseId, releaseId);
            Assert.AreEqual(expectedUbr, ubr);
        }

        [TestMethod]
        public void GivenNonExistingKeyNameExpectNullValues()
        {
            // arrange
            string wrongCurrentVersionKeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion_NOT_EXISTING";
            string expectedReleaseId = null;
            string expectedUbr = null;

            List<RegistryKeyNameReturnValue> registryKeyNameReturnValues = new List<RegistryKeyNameReturnValue>();
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() { KeyName = wrongCurrentVersionKeyName, ValueName = releaseIdKeyName, ReturnValue = expectedReleaseId });
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() { KeyName = wrongCurrentVersionKeyName, ValueName = UBRkeyName, ReturnValue = expectedUbr });

            RegistryProviderMock registryProviderMock = new RegistryProviderMock(registryKeyNameReturnValues);
            MajorVersion10Properties majorVersion10Properties = new MajorVersion10Properties(registryProviderMock);

            // act
            string releaseId = majorVersion10Properties.ReleaseId;
            string ubr = majorVersion10Properties.UBR;

            // assert
            Assert.IsNull(releaseId);
            Assert.IsNull(ubr);
        }


        [TestMethod]
        public void GivenNonExistingValueNameExpectZeroValues()
        {
            // arrange
            string wrongValueName = "ThisIsWrongForeSureIndeed!";
            string expectedReleaseId = null;
            string expectedUbr = null;

            List<RegistryKeyNameReturnValue> registryKeyNameReturnValues = new List<RegistryKeyNameReturnValue>();
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() { KeyName = registryCurrentVersionKeyName, ValueName = wrongValueName, ReturnValue = expectedReleaseId });
            registryKeyNameReturnValues.Add(new RegistryKeyNameReturnValue() { KeyName = registryCurrentVersionKeyName, ValueName = wrongValueName, ReturnValue = expectedUbr });

            RegistryProviderMock registryProviderMock = new RegistryProviderMock(registryKeyNameReturnValues);
            MajorVersion10Properties majorVersion10Properties = new MajorVersion10Properties(registryProviderMock);

            // act
            string releaseId = majorVersion10Properties.ReleaseId;
            string ubr = majorVersion10Properties.UBR;

            // assert
            Assert.IsNull(releaseId);
            Assert.IsNull(ubr);
        }


    }
}
