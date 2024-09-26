using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace WIC
{
    [GeneratedComInterface]
    [Guid(IID.IErrorLog)]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public partial interface IErrorLog
    {
        void AddError(
            [MarshalAs(UnmanagedType.LPWStr)] string pszPropName,
            IntPtr pExcepInfo); // EXCEPINFO*
    }
}
