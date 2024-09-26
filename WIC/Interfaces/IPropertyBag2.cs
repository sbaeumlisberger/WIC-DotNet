using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC;

[GeneratedComInterface]
[Guid(IID.IPropertyBag2)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IPropertyBag2
{
    void Read(
        uint cProperties,
        [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPBAG2[] pPropBag,
        IErrorLog? pErrLog,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] pvarValue,
        [Out, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] int[] phrError);

    void Write(
        uint cProperties,
        [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPBAG2[] pPropBag,
        [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPVARIANT[] pvarValue);

    void CountProperties(out uint pcProperties);

    void GetPropertyInfo(
        uint iProperty, 
        uint cProperties,
        [In, MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 0)] PROPBAG2[] pPropBag,
        out uint pcProperties);

    void LoadObject(
        [MarshalAs(UnmanagedType.LPWStr)] string pstrName,
        uint dwHint,
        IntPtr pUnkObject,
        IErrorLog? pErrLog);
}
