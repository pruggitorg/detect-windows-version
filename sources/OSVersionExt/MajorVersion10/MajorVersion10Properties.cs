using OSVersionExt.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSVersionExt.MajorVersion10
{
    public readonly struct RegistryEntry
    {
        /// <summary>
        /// The full registry path of the key, beginning with a valid registry root, such as "HKEY_CURRENT_USER".
        /// </summary>
        public string FullPathToKey { get; }

        /// <summary>
        /// The name of the name/value pair.
        /// </summary>
        public string ValueName { get; }

        /// <summary>
        /// The value to return if ValueName does not exist.
        /// </summary>
        public string DefaultValueNotFound  {get;}

        public RegistryEntry(string fullPathToKey, string valueName, string defaultValue)
        {
            FullPathToKey = fullPathToKey;
            ValueName = valueName;
            DefaultValueNotFound = defaultValue;
        }
    }

    /// <summary>
    /// Get the release id and UBR (Update Build Revision) on Windows system having major version 10.
    /// </summary>
    public class MajorVersion10Properties
    {
        private const string FullPathToCurrentVersion = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion";
        private readonly RegistryEntry _releaseIdRegistry = new RegistryEntry(FullPathToCurrentVersion, "ReleaseId", null);
        private readonly RegistryEntry _ubrRegistry = new RegistryEntry(FullPathToCurrentVersion, "UBR", null);
        private readonly RegistryEntry _displayVersionRegistry = new RegistryEntry(FullPathToCurrentVersion, "DisplayVersion", null);

        private IRegistry _registryProvider;

        private string _releaseId = null;
        private string _UBR = null;
        private string _displayVersion = null;

        /// <summary>
        /// Returns the Windows numeric release ID (e.g. 1909, 2004, 2009). For versions like 20H2 use DisplayVersion.
        /// </summary>
        /// <remarks>returns the release id or null, if detection has failed.</remarks>
        public string ReleaseId { get => _releaseId; }

        /// <summary>
        /// Gets the Update Build Revision of a Windows 10 system
        /// </summary>
        /// <remarks>returns null, if detection has failed.</remarks>
        public string UBR { get => _UBR; }

        /// <summary>
        /// Gets the Display Version such as 1909, 2004, 20H2.
        /// </summary>
        public string DisplayVersion { get => _displayVersion; }

        /// <summary>
        /// Create instance with custom registry provider.
        /// </summary>
        /// <param name="registryProvider"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public MajorVersion10Properties(IRegistry registryProvider)
        {
            _ = registryProvider ?? throw new ArgumentNullException();

            _registryProvider = registryProvider;
            GetAllProperties();
        }

        public MajorVersion10Properties()
        {
            _registryProvider = new RegistryProviderDefault();
            GetAllProperties();
        }

        private void GetAllProperties()
        {
            _releaseId = GetReleaseId();
            _UBR = GetUBR();
            _displayVersion = GetDisplayVersion();
        }

        /// <summary>        
        /// The version number representing feature updates, is referred as the release id, such as 1903, 1909.
        /// </summary>
        /// <returns>Returns the release id or null, if value is not available.</returns>
        /// <remarks>Feature updates for Windows 10 are released twice a year, around March and September, via the Semi-Annual Channel.</remarks>
        private string GetReleaseId()
        { 
            return _registryProvider.GetValue(_releaseIdRegistry.FullPathToKey, _releaseIdRegistry.ValueName, _releaseIdRegistry.DefaultValueNotFound)?.ToString();
        }

        /// <summary>
        /// Gets the  UBR (Update Build Revision).
        /// </summary>
        /// <returns></returns>
        /// <remarks>E.g, it returns 778 for Microsoft Windows [Version 10.0.18363.778] </remarks>
        private string GetUBR()
        {
            return _registryProvider.GetValue(_ubrRegistry.FullPathToKey, _ubrRegistry.ValueName, _ubrRegistry.DefaultValueNotFound)?.ToString();
        }


        /// <summary>
        /// Returns the DisplayVersion such as 20H2 (for ReleaseId 2009). If value is not found, it will return the ReleaseId.
        /// </summary>
        /// <returns></returns>
        private string GetDisplayVersion()
        {
           string displayVersion = _registryProvider.GetValue(_displayVersionRegistry.FullPathToKey, _displayVersionRegistry.ValueName, _displayVersionRegistry.DefaultValueNotFound)?.ToString();

            return displayVersion ?? GetReleaseId();
        }
    }
}
