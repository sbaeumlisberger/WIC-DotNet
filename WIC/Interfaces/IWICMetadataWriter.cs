using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataWriter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataWriter : IWICMetadataReader
    {


        void SetValue(
            PROPVARIANT pvarSchema,
            PROPVARIANT pvarId,
            PROPVARIANT pvarValue);

        void SetValueByIndex(
            int nIndex,
            PROPVARIANT pvarSchema,
            PROPVARIANT pvarId,
            PROPVARIANT pvarValue);

        void RemoveValue(
            PROPVARIANT pvarSchema,
            PROPVARIANT pvarId);

        void RemoveValueByIndex(
            int nIndex);
    }
}
