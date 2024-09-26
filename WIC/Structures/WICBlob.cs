namespace WIC
{
    public class WICBlob
    {
        public byte[] Bytes { get; }

        public WICBlob(byte[] bytes)
        {
            Bytes = bytes;
        }
    }
}
