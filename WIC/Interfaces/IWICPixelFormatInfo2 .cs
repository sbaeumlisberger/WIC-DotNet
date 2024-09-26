using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICPixelFormatInfo2)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICPixelFormatInfo2 : IWICPixelFormatInfo
    {
        [return: MarshalAs(UnmanagedType.Bool)]
        bool SupportsTransparency();

        WICPixelFormatNumericRepresentation GetNumericRepresentation();

    }
}
