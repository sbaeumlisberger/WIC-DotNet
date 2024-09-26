using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IWICPalette)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IWICPalette
    {
        void InitializePredefined(
            WICBitmapPaletteType ePaletteType,
            [MarshalAs(UnmanagedType.Bool)] bool fAddTransparentColor);

        void InitializeCustom(
            [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] int[] pColors,
            int cCount);

        void InitializeFromBitmap(
            IWICBitmapSource pISurface,
            int cCount,
            [MarshalAs(UnmanagedType.Bool)] bool fAddTransparentColor);

        void InitializeFromPalette(
            IWICPalette pIPalette);

        WICBitmapPaletteType GetType();

        int GetColorCount();

        void GetColors(
            int cCount,
            [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[]? pColors,
            out int pcActualColors);

        [return: MarshalAs(UnmanagedType.Bool)]
        bool IsBlackWhite();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool IsGrayscale();

        [return: MarshalAs(UnmanagedType.Bool)]
        bool HasAlpha();
    }
}
