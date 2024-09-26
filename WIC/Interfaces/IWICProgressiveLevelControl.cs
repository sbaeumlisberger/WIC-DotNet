using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC;

[GeneratedComInterface]
[Guid(IID.IWICProgressiveLevelControl)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
public partial interface IWICProgressiveLevelControl
{   
    uint GetCurrentLevel();

    uint GetLevelCount();

    [PreserveSig]
    int SetCurrentLevel(uint nLevel);
}
