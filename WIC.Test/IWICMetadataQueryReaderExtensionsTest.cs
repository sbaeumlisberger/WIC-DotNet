using System.Text;

namespace WIC.Test
{
    public class IWICMetadataQueryReaderExtensionsTest
    {
        [Fact]
        public void GetMetadataByName()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImage.jpg");

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite))
            {
                var wic = new WICImagingFactory();
                var decoder = wic.CreateDecoderFromStream(stream, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
                var metadataReader = decoder.GetFrame(0).GetMetadataQueryReader();
                var value = metadataReader.GetMetadataByName("System.GPS.LatitudeNumerator");
                Assert.NotNull(value);
            }
        }
    }
}