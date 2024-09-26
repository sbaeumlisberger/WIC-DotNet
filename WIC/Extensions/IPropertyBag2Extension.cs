using System.Runtime.InteropServices;

namespace WIC;

public static partial class IPropertyBag2Extension
{
    public static object? Read(this IPropertyBag2 propertyBag2, string propertyName)
    {
        PROPBAG2 propBag = new PROPBAG2();
        propBag.pstrName = Marshal.StringToCoTaskMemUni(propertyName);
        PROPVARIANT[] values = new PROPVARIANT[1];
        int[] errors = new int[1];

        try
        {
            propertyBag2.Read(1, [propBag], null, values, errors);
            Marshal.ThrowExceptionForHR(errors[0]);
            return PropVariantHelper.Decode(values[0]);
        }
        finally
        {
            Marshal.FreeCoTaskMem(propBag.pstrName);
            PropVariantHelper.Free(ref values[0]);
        }
    }


    public static void Write(this IPropertyBag2 propertyBag2, string propertyName, object? value)
    {
        PROPBAG2 propBag = new PROPBAG2();
        propBag.pstrName = Marshal.StringToCoTaskMemUni(propertyName);
        PROPVARIANT variant = PropVariantHelper.Encode(value);

        try
        {
            propertyBag2.Write(1, [propBag], [variant]);
        }
        finally
        {
            Marshal.FreeCoTaskMem(propBag.pstrName);
            PropVariantHelper.Free(ref variant);
        }
    }
}