using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICPixelFormatInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICPixelFormatInfo : IWICComponentInfo
    {
        Guid GetFormatGUID();

        [return: MarshalAs(UnmanagedType.Interface)]
        IWICColorContext GetColorContext();

        int GetBitsPerPixel();

        int GetChannelCount();

        void GetChannelMask(
            int uiChannelIndex,
            int cbMaskBuffer,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U1, SizeParamIndex = 1)] byte[] pbMaskBuffer,
            out int pcbActual);

    }
}
