namespace WIC
{
    public struct WICSize
    {
        public WICSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
