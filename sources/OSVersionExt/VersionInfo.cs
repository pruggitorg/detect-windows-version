using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSVersionExt
{
    public class VersionInfo
    {
        public Version Version { get; private set; }

        public VersionInfo(int major, int minor, int build)
        {
            this.Version = new Version(major, minor, build);            
        }
    }
   
}
