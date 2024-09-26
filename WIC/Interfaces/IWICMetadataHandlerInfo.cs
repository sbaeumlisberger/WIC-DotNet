using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataHandlerInfo)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataHandlerInfo : IWICComponentInfo
    {


        Guid GetMetadataFormat();

        void GetContainerFormats(
             int cContainerFormats,
             [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] Guid[]? pguidContainerFormats,
             out int pcchActual);

        void GetDeviceManufacturer(
            int cchDeviceManufacturer,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzDeviceManufacturer,
            out int pcchActual);

        void GetDeviceModels(
            int cchDeviceModels,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeParamIndex = 0)] char[]? wzDeviceModels,
            out int pcchActual);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesRequireFullStream();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesSupportPadding();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool DoesRequireFixedSize();
    }
}
