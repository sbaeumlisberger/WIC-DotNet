using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WIC.Test
{
    public class IPropertyBag2ExtensionTest
    {
        [Fact]
        public void Write()
        {
            var wic = WICImagingFactory.Create();

            var encoder = wic.CreateEncoder(ContainerFormat.Jpeg);

            using (var stream = File.Create("test.jpg"))
            {
                encoder.Initialize(stream, WICBitmapEncoderCacheOption.WICBitmapEncoderNoCache);

                var frame = encoder.CreateNewFrame(out IPropertyBag2 options);

                options.Write("ImageQuality", 0.4f);

                Assert.Equal(0.4f, options.Read("ImageQuality"));
            }
        }
    }
}
