using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace knoledge_spv
{
    public static class NativeCalls
    {
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int description, int reservedValue);
    }
}
