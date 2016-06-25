﻿using System;
using System.Runtime.InteropServices;

namespace stakx.WIC.Interop
{
    [ComImport]
    [Guid(IID.IWICImagingFactory)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public unsafe interface IWICImagingFactory
    {
        unsafe void CreateDecoderFromFilename(
            [In, MarshalAs(UnmanagedType.LPWStr)] string wzFilename,
            [In] Guid* pguidVendor,
            [In] StreamAccessMode dwDesiredAccess,
            [In] WICDecodeOptions metadataOptions,
            [Out] out IWICBitmapDecoder ppIDecoder);

        unsafe void CreateDecoderFromStream(
            [In] IStream pIStream,
            [In] Guid* pguidVendor,
            [In] WICDecodeOptions metadataOptions,
            [Out] out IWICBitmapDecoder ppIDecoder);

        unsafe void CreateDecoderFromFileHandle(
            [In] IntPtr hFile,
            [In] Guid* pguidVendor,
            [In] WICDecodeOptions metadataOptions,
            [Out] out IWICBitmapDecoder ppIDecoder);

        void CreateComponentInfo(
            [In] Guid clsidComponent,
            [Out] out IWICComponentInfo ppIInfo);

        unsafe void CreateDecoder(
            [In] Guid guidContainerFormat,
            [In] Guid* pguidVendor,
            [Out] out IWICBitmapDecoder ppIDecoder);

        #warning `IWICImagingFactory.CreateEncoder` is incomplete.
        void CreateEncoder();

        void CreatePalette(
            [Out] out IWICPalette ppIPalette);

        #warning `IWICImagingFactory.CreateFormatConverter` is incomplete.
        void CreateFormatConverter();

        #warning `IWICImagingFactory.CreateBitmapScaler` is incomplete.
        void CreateBitmapScaler();

        #warning `IWICImagingFactory.CreateBitmapClipper` is incomplete.
        void CreateBitmapClipper();

        #warning `IWICImagingFactory.CreateBitmapFlipRotator` is incomplete.
        void CreateBitmapFlipRotator();

        void CreateStream(
            [Out] out IWICStream ppIWICStream);

        void CreateColorContext(
            [Out] out IWICColorContext ppIWICColorContext);

        #warning `IWICImagingFactory.CreateColorTransformer` is incomplete.
        void CreateColorTransformer();

        void CreateBitmap(
            [In] int uiWidth,
            [In] int uiHeight,
            [In] Guid pixelFormat,
            [In] WICBitmapCreateCacheOption option,
            [Out] out IWICBitmap ppIBitmap);

        void CreateBitmapFromSource(
            [In] IWICBitmapSource pIBitmapSource,
            [In] WICBitmapCreateCacheOption option,
            [Out] out IWICBitmap ppIBitmap);

        void CreateBitmapFromSourceRect(
            [In] IWICBitmapSource pIBitmapSource,
            [In] int x,
            [In] int y,
            [In] int width,
            [In] int height,
            [Out] out IWICBitmap ppIBitmap);

        unsafe void CreateBitmapFromMemory(
            [In] int uiWidth,
            [In] int uiHeight,
            [In] Guid pixelFormat,
            [In] int cbStride,
            [In] int cbBufferSize,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 4)] byte* pbBuffer,
            [Out] out IWICBitmap ppIBitmap);

        void CreateBitmapFromHBITMAP(
            [In] IntPtr hBitmap,
            [In] IntPtr hPalette,
            [In] WICBitmapAlphaChannelOption options,
            [Out] out IWICBitmap ppIBitmap);

        void CreateBitmapFromHICON(
            [In] IntPtr hIcon,
            [Out] out IWICBitmap ppIBitmap);

        #warning `IWICImagingFactory.CreateComponentEnumerator` is incomplete.
        void CreateComponentEnumerator();

        #warning `IWICImagingFactory.CreateFastMetadataEncoderFromDecoder` is incomplete.
        void CreateFastMetadataEncoderFromDecoder();

        #warning `IWICImagingFactory.CreateFastMetadataEncoderFromFrameDecode` is incomplete.
        void CreateFastMetadataEncoderFromFrameDecode();

        #warning `IWICImagingFactory.CreateQueryWriter` is incomplete.
        void CreateQueryWriter();

        #warning `IWICImagingFactory.CreateQueryWriterFromReader` is incomplete.
        void CreateQueryWriterFromReader();
    }
}