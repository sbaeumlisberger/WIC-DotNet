using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapSource)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapSource
    {
        void GetSize(
            out int puiWidth,
            out int puiHeight);

        Guid GetPixelFormat();

        void GetResolution(
            out double pDpiX,
            out double pDpiY);

        void CopyPalette(
            IWICPalette pIPalette);

        void CopyPixels(
            IntPtr prc, // WICRect*
            int cbStride,
            int cbBufferSize,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] pbBuffer);
    }
}
