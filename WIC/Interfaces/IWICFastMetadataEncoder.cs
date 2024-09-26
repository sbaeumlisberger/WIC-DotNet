using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICFastMetadataEncoder)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICFastMetadataEncoder
    {
        void Commit();

        IWICMetadataQueryWriter GetMetadataQueryWriter();
    }
}
