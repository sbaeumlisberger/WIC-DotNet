using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapDecoder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapDecoder
    {
        WICBitmapDecoderCapabilities QueryCapability(
            IStream pIStream);

        void Initialize(
            IStream pIStream,
            WICDecodeOptions cacheOptions);

        Guid GetContainerFormat();

        IWICBitmapDecoderInfo GetDecoderInfo();

        void CopyPalette(
            IWICPalette pIPalette);

        IWICMetadataQueryReader GetMetadataQueryReader();

        IWICBitmapSource GetPreview();

        void GetColorContexts(
            int cCount,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[]? ppIColorContexts,
            out int pcActualCount);

        IWICBitmapSource GetThumbnail();

        int GetFrameCount();

        IWICBitmapFrameDecode GetFrame(
            int index);
    }
}
