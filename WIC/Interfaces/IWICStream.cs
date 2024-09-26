using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICStream)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICStream : IStream
    {
        void InitializeFromIStream(
            IStream pIStream);

        void InitializeFromFilename(
            [MarshalAs(UnmanagedType.LPWStr)] string wzFileName,
            StreamAccessMode dwDesiredAccess);

        void InitializeFromMemory(
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pbBuffer,
            int cbBufferSize);

        void InitializeFromIStreamRegion(
            IStream pIStream,
            long ulOffset,
            long ulMaxSize);
    }
}
