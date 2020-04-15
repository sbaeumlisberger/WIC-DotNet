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
            return FetchIntoBufferHelper.FetchArray<IWICColorContext>(bitmapFrameDecode.GetColorContexts);
        }

        public static void Initialize(this IWICBitmapFrameEncode bitmapFrameEncode, IPropertyBag2 pIEncoderOptions = null)
        {
            bitmapFrameEncode.Initialize(pIEncoderOptions);
        }

    }
}
