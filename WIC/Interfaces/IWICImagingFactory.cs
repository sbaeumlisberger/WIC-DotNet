using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICImagingFactory)]
    public partial interface IWICImagingFactory
    {
        IWICBitmapDecoder CreateDecoderFromFilename(
            [MarshalAs(UnmanagedType.LPWStr)] string wzFilename,
            IntPtr pguidVendor,
            StreamAccessMode dwDesiredAccess,
            WICDecodeOptions metadataOptions);

        IWICBitmapDecoder CreateDecoderFromStream(
            IStream pIStream,
            IntPtr pguidVendor,
            WICDecodeOptions metadataOptions);

        IWICBitmapDecoder CreateDecoderFromFileHandle(
            IntPtr hFile,
            IntPtr pguidVendor,
            WICDecodeOptions metadataOptions);

        IWICComponentInfo CreateComponentInfo(
            Guid clsidComponent);

        IWICBitmapDecoder CreateDecoder(
            Guid guidContainerFormat,
            IntPtr pguidVendor);

        IWICBitmapEncoder CreateEncoder(
            Guid guidContainerFormat,
            IntPtr pguidVendor);

        IWICPalette CreatePalette();

        IWICFormatConverter CreateFormatConverter();

        IWICBitmapScaler CreateBitmapScaler();

        IWICBitmapClipper CreateBitmapClipper();

        IWICBitmapFlipRotator CreateBitmapFlipRotator();

        IWICStream CreateStream();

        IWICColorContext CreateColorContext();

        IWICColorTransform CreateColorTransformer();

        IWICBitmap CreateBitmap(
            int uiWidth,
            int uiHeight,
            Guid pixelFormat,
            WICBitmapCreateCacheOption option);

        IWICBitmap CreateBitmapFromSource(
            IWICBitmapSource pIBitmapSource,
            WICBitmapCreateCacheOption option);

        IWICBitmap CreateBitmapFromSourceRect(
            IWICBitmapSource pIBitmapSource,
            int x,
            int y,
            int width,
            int height);

        IWICBitmap CreateBitmapFromMemory(
            int uiWidth,
            int uiHeight,
            Guid pixelFormat,
            int cbStride,
            int cbBufferSize,
            [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 4)] byte[] pbBuffer);

        IWICBitmap CreateBitmapFromHBITMAP(
            IntPtr hBitmap,
            IntPtr hPalette,
            WICBitmapAlphaChannelOption options);

        IWICBitmap CreateBitmapFromHICON(
            IntPtr hIcon);

        IEnumUnknown CreateComponentEnumerator(
            WICComponentType componentTypes,
            WICComponentEnumerateOptions options);

        IWICFastMetadataEncoder CreateFastMetadataEncoderFromDecoder(
            IWICBitmapDecoder pIDecoder);

        IWICFastMetadataEncoder CreateFastMetadataEncoderFromFrameDecode(
            IWICBitmapFrameDecode pIFrameDecoder);

        IWICMetadataQueryWriter CreateQueryWriter(
            Guid guidMetadataFormat,
            IntPtr pguidVendor);

        IWICMetadataQueryWriter CreateQueryWriterFromReader(
            IWICMetadataQueryReader pIQueryReader,
            IntPtr pguidVendor);
    }
}
