using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    /// <summary>
    /// Exposes methods for setting or removing metadata blocks and items to an encoder or its image frames using a metadata query expression.
    /// </summary>
    [GeneratedComInterface]
    [Guid(IID.IWICMetadataQueryWriter)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICMetadataQueryWriter : IWICMetadataQueryReader
    {
        /// <summary>
        /// Sets a metadata item to a specific location.
        /// </summary>       
        /// <param name="wzName">The name of the metadata item.</param>
        /// <param name="pvarValue">The metadata to set.</param>
        /// <remarks>
        /// SetMetadataByName uses metadata query expressions to set metadata. For more information on the metadata query language, see the Metadata Query Language Overview.
        /// <br/>
        /// If the value set is a nested metadata block then use variant type VT_UNKNOWN and pvarValue pointing to the IWICMetadataQueryWriter of the new metadata block.
        /// <br/>
        /// The ordering of metadata items is at the discretion of the query writer since relative locations are not specified.
        /// </remarks>
        void SetMetadataByName([MarshalAs(UnmanagedType.LPWStr)] string wzName, ref PROPVARIANT pvarValue);

        /// <summary>
        /// Removes a metadata item from a specific location using a metadata query expression.
        /// </summary>      
        /// <param name="wzName">The name of the metadata item to remove.</param>
        /// <remarks>
        /// RemoveMetadataByName uses metadata query expressions to remove metadata. For more information on the metadata query language, see the Metadata Query Language Overview.
        /// <br/>
        /// If the metadata item is a metadata block, it is removed from the metadata hierarchy.
        /// </remarks>
        void RemoveMetadataByName([MarshalAs(UnmanagedType.LPWStr)] string wzName);
    }
}
