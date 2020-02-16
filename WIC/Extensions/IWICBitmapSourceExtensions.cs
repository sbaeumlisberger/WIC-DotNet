using System;
using System.ComponentModel;

namespace WIC
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICBitmapSourceExtensions
    {
        public static void CopyPixels(this IWICBitmapSource bitmapSource, int cbStride, byte[] pbBuffer, WICRect? prc = null)
        {
            using (var prcPtr = CoTaskMemPtr.From(prc))
            {
                bitmapSource.CopyPixels(prcPtr, cbStride, pbBuffer.Length, pbBuffer);
            }
        }

        public static byte[] GetPixels(this IWICBitmapSource bitmapSource) 
        {
            var wic = new WICImagingFactory();
            Guid pixelFormat = bitmapSource.GetPixelFormat();
            var pixelFormatInfo = (IWICPixelFormatInfo)wic.CreateComponentInfo(pixelFormat);
            int bytesPerPixel = pixelFormatInfo.GetBitsPerPixel() / 8;
            bitmapSource.GetSize(out int width, out int height);
            int stride = width * bytesPerPixel;
            byte[] buffer = new byte[width * height * bytesPerPixel];
            bitmapSource.CopyPixels(stride, buffer);
            return buffer;
        }

        public static Size GetSize(this IWICBitmapSource bitmapSource)
        {
            bitmapSource.GetSize(out int width, out int height);
            return new Size(width, height);
        }

        public static Resolution GetResolution(this IWICBitmapSource bitmapSource)
        {
            bitmapSource.GetResolution(out double dpiX, out double dpiY);
            return new Resolution(dpiX, dpiY);
        }
    }
}
