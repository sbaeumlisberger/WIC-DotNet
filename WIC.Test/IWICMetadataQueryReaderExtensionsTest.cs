using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace WIC.Test
{
    public class IWICMetadataQueryReaderExtensionsTest
    {
        [Fact]
        public void GetMetadataByName()
        {
            ReadMetadata("TestImage.jpg", metadataReader =>
            {
                var value = metadataReader.GetMetadataByName("System.Keywords");
                Assert.IsType<string[]>(value);
            });
        }

        [Fact]
        public void GetMetadataByName_Throws_WhenPropertyFound()
        {
            ReadMetadata("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.Throws<COMException>(() => metadataReader.GetMetadataByName("System.Keywords"));
            });
        }

        [Fact]
        public void TryGetMetadataByName_ReturnTrue_WhenPropertyFound()
        {
            ReadMetadata("TestImage.jpg", metadataReader =>
            {
                Assert.True(metadataReader.TryGetMetadataByName("System.Keywords", out var value));
                Assert.IsType<string[]>(value);
            });
        }

        [Fact]
        public void TryGetMetadataByName_ReturnsFalse_WhenPropertyNotFound()
        {
            ReadMetadata("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.False(metadataReader.TryGetMetadataByName("System.Keywords", out var value));
                Assert.Null(value);
            });
        }

        [Fact]
        public void TryGetMetadataByName_Throws_WhenPropertyNotSupported()
        {
            ReadMetadata("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.Throws<COMException>(() => metadataReader.TryGetMetadataByName("Property.Not.Supported", out var value));
            });
        }

        private void ReadMetadata(string fileName, Action<IWICMetadataQueryReader> readMetadata)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
            var wic = new WICImagingFactory();
            var decoder = wic.CreateDecoderFromStream(stream, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
            var metadataReader = decoder.GetFrame(0).GetMetadataQueryReader();
            readMetadata(metadataReader);
        }
    }
}