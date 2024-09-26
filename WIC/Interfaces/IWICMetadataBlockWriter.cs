using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataBlockWriter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataBlockWriter : IWICMetadataBlockReader
    {


        void InitializeFromBlockReader(
            IWICMetadataBlockReader pIMDBlockReader);

        IWICMetadataWriter GetWriterByIndex(
            int nIndex);

        void AddWriter(
            IWICMetadataWriter pIMetadataWriter);

        void SetWriterByIndex(
            int nIndex,
            IWICMetadataWriter pIMetadataWriter);

        void RemoveWriterByIndex(
            int nIndex);
    }
}
