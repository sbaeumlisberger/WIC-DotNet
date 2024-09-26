using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.ISequentialStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface ISequentialStream
    {
        void Read(
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pv,
            int cb,
            IntPtr pcbRead);

        void Write(
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pv,
            int cb,
            IntPtr pcbWritten);
    }
}
