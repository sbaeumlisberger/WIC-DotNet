using System.IO;
using WIC;

namespace EncoderOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            const int width = 256;
            const int height = 256;
            const int bytesPerPixel = 3;

            var wic = WICImagingFactory.Create();

            var encoder = wic.CreateEncoder(ContainerFormat.Jpeg);

            using (var stream = File.Create("result.jpeg"))
            {
                encoder.Initialize(stream, WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

                var frame = encoder.CreateNewFrame(out IPropertyBag2 options);

                options.Write("ImageQuality", 0.1f); // set image quality encoder option

                frame.Initialize(options);

                frame.SetPixelFormat(WICPixelFormat.WICPixelFormat24bppBGR);
                frame.SetResolution(new Resolution(96, 96));
                frame.SetSize(width, height);

                var imageData = new byte[width * height * bytesPerPixel];

                // create a RGB gradient image
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        imageData[(y * width + x) * bytesPerPixel + 0] = (byte)x;           // blue
                        imageData[(y * width + x) * bytesPerPixel + 1] = (byte)y;           // green
                        imageData[(y * width + x) * bytesPerPixel + 2] = (byte)(255 - y);   // red
                    }
                }

                // write it to the frame
                IWICBitmapFrameEncodeExtensions.WritePixels(frame, height, width * bytesPerPixel, imageData);

                // commit everything to stream
                frame.Commit();
                encoder.Commit();
            }

        }
    }
}
