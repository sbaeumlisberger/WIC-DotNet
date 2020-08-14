using System;
using System.IO;
using WIC;

namespace InPlaceMetadataEncoding
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Encoding metadata in-place...");
            try
            {               
                string filePath = args[0];

                var wic = new WICImagingFactory();

                using var fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

                var decoder = wic.CreateDecoderFromStream(fileStream.AsCOMStream(), WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
                
                var frame = decoder.GetFrame(0);

                var mtadataEncoder = wic.CreateFastMetadataEncoderFromFrameDecode(frame);

                var metadataWriter = mtadataEncoder.GetMetadataQueryWriter();

                metadataWriter.SetMetadataByName("System.Keywords", new string[] { "in-place", "metadata", "encoding" });

                mtadataEncoder.Commit();

                Console.WriteLine("Metadata successfully encoded!");
            }
            catch (Exception ex) when (ex.HResult == WinCodecError.PROPERTY_NOT_SUPPORTED)
            {
                Console.WriteLine("The file format does not support the requested metadata.");
            }
            catch (Exception ex) when (ex.HResult == WinCodecError.TOO_MUCH_METADATA
                || ex.HResult == WinCodecError.INSUFFICIENT_BUFFER
                || ex.HResult == WinCodecError.IMAGE_METADATA_HEADER_UNKNOWN
                || ex.HResult == WinCodecError.UNSUPPORTED_OPERATION)
            {
                Console.WriteLine("The file has not enough padding for the requested metadata.");
            }
            Console.ReadKey();
        }
    }
}
