using System;
using System.Runtime.InteropServices;

namespace WIC
{
    [StructLayout(LayoutKind.Sequential)]
    public struct STATSTG
    {
        public IntPtr pwcsName;
        public STGTY type;
        public long cbSize;
        public long mtime;
        public long ctime;
        public long atime;
        public STGM grfMode;
        public LOCKTYPE grfLocksSupported;
        public Guid clsid;
        public int grfStateBits;
    }
}
