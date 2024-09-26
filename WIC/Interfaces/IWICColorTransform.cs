using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICColorTransform)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICColorTransform : IWICBitmapSource
    {
        void Initialize(
            IWICBitmapSource pIBitmapSource,
            IWICColorContext pIContextSource,
            IWICColorContext pIContextDest,
            Guid pixelFmtDest);
    }
}
