using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICEnumMetadataItem)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICEnumMetadataItem
    {
        void Next(
           int celt,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltSchema,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltId,
           [In, Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] rgeltValue,
           out int pceltFetched);

        void Skip(
            int celt);

        void Reset();

        IWICEnumMetadataItem Clone();
    }
}
