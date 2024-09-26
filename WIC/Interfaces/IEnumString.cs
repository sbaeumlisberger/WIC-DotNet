using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IEnumString)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IEnumString
    {
        void Next(
            int celt,
            [In, Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPWStr, SizeParamIndex = 0)] string[] rgelt,
            out int pceltFetched);

        void Skip(
            int celt);

        void Reset();

        IEnumString Clone();
    }
}
