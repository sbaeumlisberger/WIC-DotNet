using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapEncoder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapEncoder
    {
        void Initialize(
            IStream pIStream,
            WICBitmapEncoderCacheOption cacheOption);

        Guid GetContainerFormat();

        IWICBitmapEncoderInfo GetEncoderInfo();

        void SetColorContexts(
            int cCount,
            [In, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[] ppIColorContext);

        void SetPalette(
            IWICPalette pIPalette);

        void SetThumbnail(
            IWICBitmapSource pIThumbnail);

        void SetPreview(
            IWICBitmapSource pIPreview);

        void CreateNewFrame(
            out IWICBitmapFrameEncode ppIFrameEncode,
            out IPropertyBag2 ppIEncoderOptions);

        void Commit();

        IWICMetadataQueryWriter GetMetadataQueryWriter();
    }
}
