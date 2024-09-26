using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICBitmapCodecInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICBitmapCodecInfo : IWICComponentInfo
    {


        Guid GetContainerFormat();

        void GetPixelFormats(
            int cFormats,
            [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] Guid[]? pguidPixelFormats,
            out int pcActual);

        void GetColorManagementVersion(
            int cchColorManagementVersion,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzColorManagementVersion,
            out int pcchActual);

        void GetDeviceManufacturer(
            int cchDeviceManufacturer,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzDeviceManufacturer,
            out int pcchActual);

        void GetDeviceModels(
            int cchDeviceModels,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzDeviceModels,
            out int pcchActual);

        void GetMimeTypes(
            int cchMimeTypes,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzMimeTypes,
            out int pcchActual);

        void GetFileExtensions(
            int cchFileExtensions,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzFileExtensions,
            out int pcchActual);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesSupportAnimation();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesSupportChromakey();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesSupportLossless();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesSupportMultiframe();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool MatchesMimeType(
            [MarshalAs(UnmanagedType.LPWStr)] string wzMimeType);
    }
}
