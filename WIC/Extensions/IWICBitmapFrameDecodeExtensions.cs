using System.ComponentModel;

namespace WIC
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICBitmapFrameDecodeExtensions
    {
        public static IWICMetadataBlockReader AsMetadataBlockReader(this IWICBitmapFrameDecode bitmapFrameDecode)
        {
            return bitmapFrameDecode as IWICMetadataBlockReader;
        }

        public static IWICColorContext[] GetColorContexts(this IWICBitmapFrameDecode bitmapFrameDecode)
        {
            var wic = new WICImagingFactory();

            bitmapFrameDecode.GetColorContexts(0, null, out int length);
            
            var colorContexts = new IWICColorContext[length];         

            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    colorContexts[i] = wic.CreateColorContext();
                }

                bitmapFrameDecode.GetColorContexts(length, colorContexts, out _);
            }

            return colorContexts;
        }

        public static void Initialize(this IWICBitmapFrameEncode bitmapFrameEncode, IPropertyBag2 pIEncoderOptions = null)
        {
            bitmapFrameEncode.Initialize(pIEncoderOptions);
        }

    }
}
