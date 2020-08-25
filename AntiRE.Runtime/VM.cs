using System.Runtime.InteropServices;

namespace AntiRE.Runtime
{
    internal class VM
    {
        [DllImport("kernel32.dll", EntryPoint = "VirtualProtect")]
        internal unsafe static extern byte VM936799001(byte* a, int b, uint c, ref uint d);

    }
}
