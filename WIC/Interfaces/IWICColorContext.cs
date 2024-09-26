using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICColorContext)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICColorContext
    {
        void InitializeFromFilename(
            [MarshalAs(UnmanagedType.LPWStr)] string wzFilename);

        void InitializeFromMemory(
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pbBuffer,
            int cbBufferSize);

        void InitializeFromExifColorSpace(
            ExifColorSpace value);

        WICColorContextType GetType();

        void GetProfileBytes(
            int cbBuffer,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 0)] byte[]? pbBuffer,
            out int pcbActual);

        ExifColorSpace GetExifColorSpace();
    }
}
