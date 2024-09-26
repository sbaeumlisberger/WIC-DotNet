using WIC;

var wic = WICImagingFactory.Create();

var encoders = wic
    .CreateComponentEnumerator(WICComponentType.WICEncoder, WICComponentEnumerateOptions.WICComponentEnumerateDefault)
    .AsEnumerable()
    .OfType<IWICBitmapEncoderInfo>();

Console.WriteLine($"Found {encoders.Count()} encoders");

string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestImage.jpg");
using var stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);
var decoder = wic.CreateDecoderFromStream(stream, WICDecodeOptions.WICDecodeMetadataCacheOnDemand);

Console.WriteLine($"Decoder: {decoder.GetDecoderInfo().GetFileExtensions().First()}");

var metadataQueryReader = decoder.GetFrame(0).GetMetadataQueryReader();

metadataQueryReader.TryGetMetadataByName("/xmp/<xmpstruct>MP:RegionInfo/<xmpbag>MPRI:Regions", out var metadataBlock);

Console.WriteLine($"Metadata Block: {((IWICMetadataQueryReader)metadataBlock!).GetLocation()}");

Console.ReadLine();