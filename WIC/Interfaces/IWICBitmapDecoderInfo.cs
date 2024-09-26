using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapDecoderInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapDecoderInfo : IWICBitmapCodecInfo
    {
        void GetPatterns(
            int cbSizePatterns,
            IntPtr pPatterns,
            out int pcPatterns,
            out int pcbPatternsActual);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool MatchesPattern(
            IStream pIStream);

        IWICBitmapDecoder CreateInstance();
    }
}
