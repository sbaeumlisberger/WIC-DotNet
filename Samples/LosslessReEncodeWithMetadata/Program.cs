using System;
using System.IO;
using WIC;

namespace LosslessReEncodeWithMetadata
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Re-encoding image lossless with metadata...");
            try
            {
                string filePath = args[0];

                var wic = new WICImagingFactory();

                using var fileStream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

                var decoder = wic.CreateDecoderFromStream(fileStream.AsCOMStream(), WICDecodeOptions.WICDecodeMetadataCacheOnDemand/*lossless decoding/encoding*/);

                var frame = decoder.GetFrame(0);

                using var memoryStream = new MemoryStream();

                var encoder = wic.CreateEncoder(decoder.GetContainerFormat());

                encoder.Initialize(memoryStream.AsCOMStream(), WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

                var frameEncoder = encoder.CreateNewFrame();
                frameEncoder.Initialize(null);
                frameEncoder.SetSize(frame.GetSize()); // lossless decoding/encoding
                frameEncoder.SetResolution(frame.GetResolution()); // lossless decoding/encoding
                frameEncoder.SetPixelFormat(frame.GetPixelFormat()); // lossless decoding/encoding

                frameEncoder.AsMetadataBlockWriter().InitializeFromBlockReader(frame.AsMetadataBlockReader());

                var metadataWriter = frameEncoder.GetMetadataQueryWriter();

                metadataWriter.SetMetadataByName("System.Keywords", new string[] { "lossless", "re-encode", "with", "metadata" });

                frameEncoder.WriteSource(frame);

                frameEncoder.Commit();
                encoder.Commit();

                memoryStream.Flush();
                memoryStream.Position = 0;
                fileStream.Position = 0;
                fileStream.SetLength(0);
                memoryStream.CopyTo(fileStream);

                Console.WriteLine("Image successfully lossless re-encoded with metadata!");
            }
            catch (Exception ex) when (ex.HResult == HResult.WINCODEC_ERR_PROPERTY_NOT_SUPPORTED)
            {
                Console.WriteLine("The file format does not support the requested metadata.");
            }
            catch (Exception ex) when (ex.HResult == HResult.WINCODEC_ERR_UNSUPPORTED_OPERATION)
            {
                Console.WriteLine("The file format does not support any metadata.");
            }
            Console.ReadKey();
        }
    }
}
