using OSVersionExt.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace OSVersionExtensionTests.Mocks
{
    public struct  RegistryKeyNameReturnValue
    {        
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public string ReturnValue { get; set; }
    }

    public class RegistryProviderMock : IRegistry
    {
        private List<RegistryKeyNameReturnValue> _registries = new List<RegistryKeyNameReturnValue>();

        public object GetValue(string keyName, string valueName, object defaultValue)
        {
            if (!this._registries.Any(x => x.KeyName == keyName))
                return null;

            if (!this._registries.Any(x => x.ValueName == valueName))
                return defaultValue;

            return this._registries.FirstOrDefault(x => x.KeyName == keyName  && x.ValueName == valueName ).ReturnValue;
        }

        public RegistryProviderMock(List<RegistryKeyNameReturnValue> registries)
        {
            this._registries = registries;
        }
    }
}
