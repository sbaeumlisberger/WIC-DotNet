using System;
using System.Runtime.InteropServices;

namespace WIC
{
    public class WICImagingFactory : IWICImagingFactory
    {
        private readonly IWICImagingFactory comObject;

        [Obsolete("Use WICImagingFactory.Create()")]
        public WICImagingFactory()
        {
            comObject = Create();
        }

        public static IWICImagingFactory Create()
        {
            Guid clsid = new Guid(CLSID.WICImagingFactory);
            Guid iid = new Guid(IID.IWICImagingFactory);
            Ole32.CoCreateInstance(clsid, IntPtr.Zero, /*CLSCTX_INPROC_SERVER*/ 1, iid, out IntPtr ptr);
            return (IWICImagingFactory)WICComWrappers.Instance.GetOrCreateObjectForComInstance(ptr, CreateObjectFlags.None);
        }

        #region IWICImagingFactory

        public IWICBitmap CreateBitmap(int uiWidth, int uiHeight, Guid pixelFormat, WICBitmapCreateCacheOption option)
        {
            return comObject.CreateBitmap(uiWidth, uiHeight, pixelFormat, option);
        }

        public IWICBitmapClipper CreateBitmapClipper()
        {
            return comObject.CreateBitmapClipper();
        }

        public IWICBitmapFlipRotator CreateBitmapFlipRotator()
        {
            return comObject.CreateBitmapFlipRotator();
        }

        public IWICBitmap CreateBitmapFromHBITMAP(nint hBitmap, nint hPalette, WICBitmapAlphaChannelOption options)
        {
            return comObject.CreateBitmapFromHBITMAP(hBitmap, hPalette, options);
        }

        public IWICBitmap CreateBitmapFromHICON(nint hIcon)
        {
            return comObject.CreateBitmapFromHICON(hIcon);
        }

        public IWICBitmap CreateBitmapFromMemory(int uiWidth, int uiHeight, Guid pixelFormat, int cbStride, int cbBufferSize, byte[] pbBuffer)
        {
            return comObject.CreateBitmapFromMemory(uiWidth, uiHeight, pixelFormat, cbStride, cbBufferSize, pbBuffer);
        }

        public IWICBitmap CreateBitmapFromSource(IWICBitmapSource pIBitmapSource, WICBitmapCreateCacheOption option)
        {
            return comObject.CreateBitmapFromSource(pIBitmapSource, option);
        }

        public IWICBitmap CreateBitmapFromSourceRect(IWICBitmapSource pIBitmapSource, int x, int y, int width, int height)
        {
            return comObject.CreateBitmapFromSourceRect(pIBitmapSource, x, y, width, height);
        }

        public IWICBitmapScaler CreateBitmapScaler()
        {
            return comObject.CreateBitmapScaler();
        }

        public IWICColorContext CreateColorContext()
        {
            return comObject.CreateColorContext();
        }

        public IWICColorTransform CreateColorTransformer()
        {
            return comObject.CreateColorTransformer();
        }

        public IEnumUnknown CreateComponentEnumerator(WICComponentType componentTypes, WICComponentEnumerateOptions options)
        {
            return comObject.CreateComponentEnumerator(componentTypes, options);
        }

        public IWICComponentInfo CreateComponentInfo(Guid clsidComponent)
        {
            return comObject.CreateComponentInfo(clsidComponent);
        }

        public IWICBitmapDecoder CreateDecoder(Guid guidContainerFormat, nint pguidVendor)
        {
            return comObject.CreateDecoder(guidContainerFormat, pguidVendor);
        }

        public IWICBitmapDecoder CreateDecoderFromFileHandle(nint hFile, nint pguidVendor, WICDecodeOptions metadataOptions)
        {
            return comObject.CreateDecoderFromFileHandle(hFile, pguidVendor, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromFilename(string wzFilename, nint pguidVendor, StreamAccessMode dwDesiredAccess, WICDecodeOptions metadataOptions)
        {
            return comObject.CreateDecoderFromFilename(wzFilename, pguidVendor, dwDesiredAccess, metadataOptions);
        }

        public IWICBitmapDecoder CreateDecoderFromStream(IStream pIStream, nint pguidVendor, WICDecodeOptions metadataOptions)
        {
            return comObject.CreateDecoderFromStream(pIStream, pguidVendor, metadataOptions);
        }

        public IWICBitmapEncoder CreateEncoder(Guid guidContainerFormat, nint pguidVendor)
        {
            return comObject.CreateEncoder(guidContainerFormat, pguidVendor);
        }

        public IWICFastMetadataEncoder CreateFastMetadataEncoderFromDecoder(IWICBitmapDecoder pIDecoder)
        {
            return comObject.CreateFastMetadataEncoderFromDecoder(pIDecoder);
        }

        public IWICFastMetadataEncoder CreateFastMetadataEncoderFromFrameDecode(IWICBitmapFrameDecode pIFrameDecoder)
        {
            return comObject.CreateFastMetadataEncoderFromFrameDecode(pIFrameDecoder);
        }

        public IWICFormatConverter CreateFormatConverter()
        {
            return comObject.CreateFormatConverter();
        }

        public IWICPalette CreatePalette()
        {
            return comObject.CreatePalette();
        }

        public IWICMetadataQueryWriter CreateQueryWriter(Guid guidMetadataFormat, nint pguidVendor)
        {
            return comObject.CreateQueryWriter(guidMetadataFormat, pguidVendor);
        }

        public IWICMetadataQueryWriter CreateQueryWriterFromReader(IWICMetadataQueryReader pIQueryReader, nint pguidVendor)
        {
            return comObject.CreateQueryWriterFromReader(pIQueryReader, pguidVendor);
        }

        public IWICStream CreateStream()
        {
            return comObject.CreateStream();
        }

        #endregion
    }

    internal partial class Ole32
    {
        [LibraryImport("Ole32")]
        public static partial void CoCreateInstance(Guid rclsid, IntPtr pUnkOuter, int dwClsContext, Guid riid, out IntPtr ppObj);
    }
}
