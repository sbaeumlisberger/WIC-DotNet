using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICFormatConverter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICFormatConverter : IWICBitmapSource
    {
        void Initialize(
            IWICBitmapSource pISource,
            Guid dstFormat,
            WICBitmapDitherType dither,
            IWICPalette? pIPalette,
            double alphaThresholdPercent,
            WICBitmapPaletteType paletteTranslate);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool CanConvert(
            Guid srcPixelFormat,
            Guid dstPixelFormat);
    }
}
