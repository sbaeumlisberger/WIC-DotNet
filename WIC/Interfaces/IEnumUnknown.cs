using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IEnumUnknown)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IEnumUnknown
    {
        void Next(
            int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Interface, SizeParamIndex = 0)] object[] rgelt,
            out int pceltFetched);

        void Skip(
            int celt);

        void Reset();

        IEnumUnknown Clone();
    }
}
