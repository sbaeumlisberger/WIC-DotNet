using System;
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
            if (metadataQueryWriter is null)
            {
                throw new NullReferenceException();
            }
            if (name is null) 
            { 
                throw new ArgumentNullException(nameof(name));
            }
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            var variant = PropVariantHelpers.Encode(value);
            try
            {
                metadataQueryWriter.SetMetadataByName(name, ref variant);
            }
            finally
            {
                Dispose(ref variant);
            }
        }

    }
}
