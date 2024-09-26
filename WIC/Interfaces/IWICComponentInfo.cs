using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICComponentInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICComponentInfo
    {
        WICComponentType GetComponentType();

        Guid GetCLSID();

        WICComponentSigning GetSigningStatus();

        void GetAuthor(
            int cchAuthor,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzAuthor,
            out int pcchActual);

        Guid GetVendorGUID();

        void GetVersion(
            int cchVersion,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzVersion,
            out int pcchActual);

        void GetSpecVersion(
            int cchSpecVersion,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzSpecVersion,
            out int pcchActual);

        void GetFriendlyName(
            int cchFriendlyName,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzFriendlyName,
            out int pcchActual);
    }
}
