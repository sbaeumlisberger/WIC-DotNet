using System.ComponentModel;
using System.Runtime.InteropServices;
using static WIC.PropVariantHelpers;

namespace WIC
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICMetadataQueryWriterExtensions
    {

        public static void SetMetadataByName(this IWICMetadataQueryWriter metadataQueryWriter, string name, object value)
        {
            var variant = PropVariantHelpers.Encode(value);
            try
            {
                metadataQueryWriter.SetMetadataByName(name, variant);        
            }          
            finally
            {
                Dispose(ref variant);
            }
        }

    }
}
