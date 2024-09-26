using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataBlockReader)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataBlockReader
    {
        Guid GetContainerFormat();

        int GetCount();

        IWICMetadataReader GetReaderByIndex(int nIndex);

        IEnumUnknown GetEnumerator();
    }
}
