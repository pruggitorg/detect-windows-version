using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSVersionExt
{
    public class VersionInfo
    {
        public int Build { get; private set; }
        public int Minor { get; private set; }
        public int Major { get; private set; }

        public VersionInfo(int major, int minor, int build)
        {
            this.Major = major;
            this.Minor = minor;
            this.Build = build;
        }
    }
}
