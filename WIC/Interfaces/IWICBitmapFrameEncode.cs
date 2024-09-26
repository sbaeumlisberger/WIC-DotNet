using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapFrameEncode)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapFrameEncode
    {
        void Initialize(
            IPropertyBag2? pIEncoderOptions = null);

        void SetSize(
            int uiWidth,
            int uiHeight);

        void SetResolution(
            double dpiX,
            double dpiY);

        void SetPixelFormat(
            Guid pPixelFormat);

        void SetColorContexts(
            int cCount,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[] ppIColorContext);

        void SetPalette(
            IWICPalette pIPalette);

        void SetThumbnail(
            IWICBitmapSource pIThumbnail);

        void WritePixels(
            int lineCount,
            int cbStride,
            int cbBufferSize,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 2)] byte[] pbPixels);

        void WriteSource(
            IWICBitmapSource pIBitmapSource,
            IntPtr prc);

        void Commit();

        IWICMetadataQueryWriter GetMetadataQueryWriter();
    }
}
