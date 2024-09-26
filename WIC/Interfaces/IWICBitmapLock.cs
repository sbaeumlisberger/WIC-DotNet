using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapLock)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapLock
    {
        void GetSize(
            out int puiWidth,
            out int puiHeight);

        int GetStride();

        IntPtr GetDataPointer( // byte*
            out int pcbBufferSize);

        Guid GetPixelFormat();
    }
}
