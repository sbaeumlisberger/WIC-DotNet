using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataReader)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataReader
    {
        Guid GetMetadataFormat();

        IWICMetadataHandlerInfo GetMetadataHandlerInfo();

        int GetCount();

        void GetValueByIndex(
            int nIndex,
            ref PROPVARIANT pvarSchema,
            ref PROPVARIANT pvarId,
            ref PROPVARIANT pvarValue);

        void GetValue(
            PROPVARIANT pvarSchema,
            PROPVARIANT pvarId,
            ref PROPVARIANT pvarValue);

        IWICEnumMetadataItem GetEnumerator();
    }
}
