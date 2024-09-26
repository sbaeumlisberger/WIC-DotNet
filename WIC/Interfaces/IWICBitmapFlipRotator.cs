using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapFlipRotator)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapFlipRotator : IWICBitmapSource
    {
        void Initialize(
            IWICBitmapSource pISource,
            WICBitmapTransformOptions options);
    }
}
