using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapScaler)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapScaler : IWICBitmapSource
    {
        void Initialize(
             IWICBitmapSource pISource,
             int uiWidth,
             int uiHeight,
             WICBitmapInterpolationMode mode);
    }
}
