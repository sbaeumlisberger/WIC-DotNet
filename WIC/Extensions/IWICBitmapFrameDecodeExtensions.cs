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
            FetchIntoBuffer<IWICColorContext> fetcher = bitmapFrameDecode.GetColorContexts;
            return fetcher.FetchArray();
        }

        public static void Initialize(this IWICBitmapFrameEncode bitmapFrameEncode, IPropertyBag2 pIEncoderOptions = null)
        {
            bitmapFrameEncode.Initialize(pIEncoderOptions);
        }

    }
}
