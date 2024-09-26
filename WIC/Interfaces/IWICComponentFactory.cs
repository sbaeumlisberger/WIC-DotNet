using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICComponentFactory)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICComponentFactory : IWICImagingFactory
    {


        IWICMetadataReader CreateMetadataReader(
            Guid guidContainerFormat,
            IntPtr pguidVendor,
            MetadataCreationAndPersistOptions dwOptions,
            IStream pIStream);

        IWICMetadataReader CreateMetadataReaderFromContainer(
            Guid guidContainerFormat,
            IntPtr pguidVendor,
            MetadataCreationAndPersistOptions dwOptions,
            IStream pIStream);

        IWICMetadataWriter CreateMetadataWriter(
            Guid guidContainerFormat,
            IntPtr pguidVendor,
            WICMetadataCreationOptions dwMetadataOptions);

        IWICMetadataWriter CreateMetadataWriterFromReader(
            IWICMetadataReader pIReader,
            IntPtr pguidVendor);

        IWICMetadataQueryReader CreateQueryReaderFromBlockReader(
            IWICMetadataBlockReader pIBlockReader);

        IWICMetadataQueryWriter CreateQueryWriterFromBlockWriter(
            IWICMetadataBlockWriter pIBlockWriter);

        IPropertyBag2 CreateEncoderPropertyBag(
            [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] PROPBAG2[] ppropOptions,
            int cCount);
    };
}
