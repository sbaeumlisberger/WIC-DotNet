using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WIC.Test;

public class ProgressiveDecodingTest
{

    [Fact]
    public void ProgressiveDecoding()
    {
        string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImageProgressive.jpg");
        var wic = WICImagingFactory.Create();
        var decoder = wic.CreateDecoderFromFilename(filePath, IntPtr.Zero, StreamAccessMode.GENERIC_READ, WICDecodeOptions.WICDecodeMetadataCacheOnLoad);
        var frame = decoder.GetFrame(0);

        var metadataReader = frame.GetMetadataQueryReader();
        if (metadataReader.TryGetMetadataByName("/app0/JFIF/Progressive", out var isProgressiveMetadata))
        {
            bool isProgressive = (bool)(isProgressiveMetadata ?? false);
            if (!isProgressive)
            {
                throw new InvalidOperationException("The image is not a progressive JPEG.");
            }
        }

        var progressive = (IWICProgressiveLevelControl)frame;

        uint currentLevel = 0;
        byte[] currentPixelArray = [];
        while (true)
        {
            int hr = progressive.SetCurrentLevel(currentLevel);
           
            if (hr == WinCodecError.INVALID_PROGRESSIVE_LEVEL)
            {
                break;
            }
            else if (hr != 0)
            {
                Marshal.ThrowExceptionForHR(hr);
            }

            var bytes = frame.GetPixels();
            Assert.NotNull(bytes);
            Assert.NotEqual(bytes, currentPixelArray);
            // Assert.True(bytes.Length > currentPixelArray.Length);
            currentPixelArray = bytes;

            currentLevel++;
        }

        Assert.Equal(10u, currentLevel);
    }
}
