using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapEncoderInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapEncoderInfo : IWICBitmapCodecInfo
    {
        IWICBitmapEncoder CreateInstance();
    }
}
