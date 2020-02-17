using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using static WIC.PropVariantHelpers;

namespace WIC
{
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static class IWICMetadataQueryReaderExtensions
    {
        public static string GetLocation(this IWICMetadataQueryReader metadataQueryReader)
        {
            FetchIntoBuffer<char> fetcher = metadataQueryReader.GetLocation;
            return fetcher.FetchString();
        }

        public static bool TryGetMetadataByName<T>(this IWICMetadataQueryReader metadataQueryReader, string name, out T value)
        {
            if (metadataQueryReader is null)
            {
                throw new NullReferenceException();
            }
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var variant = new PROPVARIANT();
            try
            {
                metadataQueryReader.GetMetadataByName(name, ref variant);
                return TryDecode(ref variant, out value);
            }
            catch (COMException ex) when (ex.ErrorCode == HResult.WINCODEC_ERR_PROPERTY_NOT_FOUND)
            {
                value = default(T);
                return false;
            }
            finally
            {
                Dispose(ref variant);
            }
        }
    }
}
