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
            GetMetadataQueryReader("TestImage.jpg", metadataReader =>
            {
                var value = metadataReader.GetMetadataByName("System.Keywords");
                Assert.IsType<string[]>(value);
            });
        }

        [Fact]
        public void SetMetadataByName()
        {
            GetMetadataQueryWriter("TestImage.jpg", metadataWriter =>
            {
                string[] keywords = ["test keyword"];
                metadataWriter.SetMetadataByName("System.Keywords", keywords);
                Assert.Equal(keywords, metadataWriter.GetMetadataByName("System.Keywords"));
            });
        }


        [Fact]
        public void GetMetadataByName_Throws_WhenPropertyFound()
        {
            GetMetadataQueryReader("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.Throws<COMException>(() => metadataReader.GetMetadataByName("System.Keywords"));
            });
        }

        [Fact]
        public void TryGetMetadataByName_ReturnTrue_WhenPropertyFound()
        {
            GetMetadataQueryReader("TestImage.jpg", metadataReader =>
            {
                Assert.True(metadataReader.TryGetMetadataByName("System.Keywords", out var value));
                Assert.IsType<string[]>(value);
            });
        }

        [Fact]
        public void TryGetMetadataByName_ReturnsFalse_WhenPropertyNotFound()
        {
            GetMetadataQueryReader("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.False(metadataReader.TryGetMetadataByName("System.Keywords", out var value));
                Assert.Null(value);
            });
        }

        [Fact]
        public void TryGetMetadataByName_Throws_WhenPropertyNotSupported()
        {
            GetMetadataQueryReader("TestImageWithoutMetadata.jpg", metadataReader =>
            {
                Assert.Throws<COMException>(() => metadataReader.TryGetMetadataByName("Property.Not.Supported", out var value));
            });
        }

        private void GetMetadataQueryReader(string fileName, Action<IWICMetadataQueryReader> readMetadata)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
            var wic = WICImagingFactory.Create();
            var decoder = wic.CreateDecoderFromStream(stream, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
            var metadataReader = decoder.GetFrame(0).GetMetadataQueryReader();
            readMetadata(metadataReader);
        }

        private void GetMetadataQueryWriter(string fileName, Action<IWICMetadataQueryWriter> writeMetadata)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            using var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
            var wic = WICImagingFactory.Create();
            var decoder = wic.CreateDecoderFromStream(stream, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);
            var encoder = wic.CreateFastMetadataEncoderFromFrameDecode(decoder.GetFrame(0));
            writeMetadata(encoder.GetMetadataQueryWriter());
        }
    }
}