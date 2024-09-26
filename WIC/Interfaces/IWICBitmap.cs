using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmap)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmap : IWICBitmapSource
    {
        IWICBitmapLock Lock(
             IntPtr prcLock, // WICRect*
             WICBitmapLockFlags flags);

        void SetPalette(
            IWICPalette pIPalette);

        void SetResolution(
            double dpiX,
            double dpiY);
    }
}
