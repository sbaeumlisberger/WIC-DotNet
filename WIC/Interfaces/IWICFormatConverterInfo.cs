using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICFormatConverterInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICFormatConverterInfo : IWICComponentInfo
    {


        void GetPixelFormats(
            int cFormats,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] Guid[]? pPixelFormatGUIDs,
            out int pcActual);

        IWICFormatConverter CreateInstance();
    }
}
