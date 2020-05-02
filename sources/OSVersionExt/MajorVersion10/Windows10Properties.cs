using OSVersionExt.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSVersionExt.MajorVersion10
{
    public class MajorVersion10Properties
    {
        private const string registryCurrentVersionKeyName = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private const string releaseIdKeyName = "ReleaseId";
        private const string UBRkeyName = "UBR";

        private IRegistry _registryProvider;


        private string _releaseId = String.Empty;
        private int _UBR = 0;

        /// <summary>
        /// Returns the Windows release ID.
        /// </summary>
        /// <remarks>returns an empty string, if detection has failed.</remarks>
        public string ReleaseId { get => _releaseId; }

        /// <summary>
        /// Gets the Update Build Revision of a Windows 10 system
        /// </summary>
        /// <remarks>returns 0, if detection has failed.</remarks>
        public int UBR { get => _UBR; }

        public MajorVersion10Properties(IRegistry registryProvider)
        {
            this._registryProvider = registryProvider;
            GetAllProperties();
        }

        public MajorVersion10Properties()
        {
            this._registryProvider = new RegistryProviderDefault();
            GetAllProperties();
        }

        private void GetAllProperties()
        {
            this._releaseId = this.GetReleaseId();
            this._UBR = this.GetUBR();
        }

        private string GetReleaseId()
        {
            return this._registryProvider.GetValue(registryCurrentVersionKeyName, releaseIdKeyName, String.Empty).ToString();
        }

        private int GetUBR()
        {
            return (int)this._registryProvider.GetValue(registryCurrentVersionKeyName, UBRkeyName,0);
        }

    }
}
