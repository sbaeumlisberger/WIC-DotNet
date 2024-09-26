using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapFrameDecode)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapFrameDecode : IWICBitmapSource
    {
        IWICMetadataQueryReader GetMetadataQueryReader();

        void GetColorContexts(
            int cCount,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] IWICColorContext[]? ppIColorContexts,
            out int pcActualCount);

        IWICBitmapSource GetThumbnail();
    }
}
