using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace WIC
{
    internal static partial class PropVariantHelper
    {
        private const VARTYPE VectorFlags = VARTYPE.VT_ARRAY | VARTYPE.VT_VECTOR | VARTYPE.VT_BYREF;

        private static readonly IReadOnlyDictionary<Type, int> elementSizes = new Dictionary<Type, int>()
        {
            [typeof(bool)] = 2,
            [typeof(byte)] = 1,
            [typeof(ushort)] = 2,
            [typeof(uint)] = 4,
            [typeof(ulong)] = 8,
            [typeof(sbyte)] = 1,
            [typeof(short)] = 2,
            [typeof(int)] = 4,
            [typeof(long)] = 8,
            [typeof(string)] = IntPtr.Size,
            [typeof(float)] = 4,
            [typeof(double)] = 8
        };

        private static readonly IReadOnlyDictionary<Type, Action<IntPtr, object>> elementEncoders = new Dictionary<Type, Action<IntPtr, object>>()
        {
            [typeof(bool)] = (ptr, value) => Marshal.WriteInt16(ptr, (bool)value ? (short)1 : (short)0),
            [typeof(byte)] = (ptr, value) => Marshal.WriteByte(ptr, (byte)value),
            [typeof(ushort)] = (ptr, value) => Marshal.WriteInt16(ptr, (short)(ushort)value),
            [typeof(uint)] = (ptr, value) => Marshal.WriteInt32(ptr, (int)(uint)value),
            [typeof(ulong)] = (ptr, value) => Marshal.WriteInt64(ptr, (long)(ulong)value),
            [typeof(sbyte)] = (ptr, value) => Marshal.WriteByte(ptr, (byte)(sbyte)value),
            [typeof(short)] = (ptr, value) => Marshal.WriteInt16(ptr, (short)value),
            [typeof(int)] = (ptr, value) => Marshal.WriteInt32(ptr, (int)value),
            [typeof(long)] = (ptr, value) => Marshal.WriteInt64(ptr, (long)value),
            [typeof(string)] = (ptr, value) => Marshal.WriteIntPtr(ptr, 0, Marshal.StringToCoTaskMemUni((string)value)),
            [typeof(float)] = (ptr, value) => Marshal.WriteInt32(ptr, BitConverter.ToInt32(BitConverter.GetBytes((float)value), 0)),
            [typeof(double)] = (ptr, value) => Marshal.WriteInt64(ptr, BitConverter.ToInt64(BitConverter.GetBytes((double)value), 0))
        };

        private static readonly IReadOnlyDictionary<Type, VARTYPE> vectorTypes = new Dictionary<Type, VARTYPE>()
        {
            [typeof(bool)] = VARTYPE.VT_BOOL,
            [typeof(byte)] = VARTYPE.VT_UI1,
            [typeof(ushort)] = VARTYPE.VT_UI2,
            [typeof(uint)] = VARTYPE.VT_UI4,
            [typeof(ulong)] = VARTYPE.VT_UI8,
            [typeof(sbyte)] = VARTYPE.VT_I1,
            [typeof(short)] = VARTYPE.VT_I2,
            [typeof(int)] = VARTYPE.VT_I4,
            [typeof(long)] = VARTYPE.VT_I8,
            [typeof(string)] = VARTYPE.VT_LPWSTR,
            [typeof(float)] = VARTYPE.VT_R4,
            [typeof(double)] = VARTYPE.VT_R8
        };

        public static object? Decode(PROPVARIANT variant)
        {
            if (variant.Type == VARTYPE.VT_BLOB)
            {
                return DecodeBlob(variant);
            }
            if ((variant.Type & VectorFlags) != 0)
            {
                return DecodeVector(variant);
            }
            else
            {
                return DecodeValue(variant);
            }
        }

        public static PROPVARIANT Encode(object? value)
        {
            if (value is null)
            {
                return new PROPVARIANT() { Type = VARTYPE.VT_EMPTY };
            }

            var type = value.GetType();

            if (value is WICBlob blob)
            {
                return EncodeBlob(blob);
            }
            else if (type.IsArray)
            {
                return EncodeVector((Array)value, type.GetElementType()!);
            }
            else
            {
                return EncodeValue(value);
            }
        }

        public static void Free(ref PROPVARIANT variant)
        {
            PropVariantClear(ref variant);
        }

        private static WICBlob DecodeBlob(PROPVARIANT variant)
        {
            byte[] bytes = new byte[variant.Vector.Length];
            if (variant.Vector.Length > 0)
            {
                Marshal.Copy(variant.Vector.Ptr, bytes, 0, variant.Vector.Length);
            }
            return new WICBlob(bytes);
        }

        private static Array DecodeVector(PROPVARIANT variant)
        {
            int elementSize;
            Func<IntPtr, object?> elementDecoder;

            int length = variant.Vector.Length;
            Array array;

            switch (variant.Type & ~VectorFlags)
            {
                case VARTYPE.VT_I1:
                    array = new sbyte[length];
                    elementDecoder = ptr => (sbyte)Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_I2:
                    array = new short[length];
                    elementDecoder = ptr => Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_I4:
                    array = new int[length];
                    elementDecoder = ptr => Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_I8:
                    array = new long[length];
                    elementDecoder = ptr => Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_UI1:
                    array = new byte[length];
                    elementDecoder = ptr => Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_UI2:
                    array = new ushort[length];
                    elementDecoder = ptr => (ushort)Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_UI4:
                    array = new uint[length];
                    elementDecoder = ptr => (uint)Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_UI8:
                    array = new ulong[length];
                    elementDecoder = ptr => (ulong)Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_LPSTR:
                    array = new string[length];
                    elementDecoder = ptr => Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(ptr))!;
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_LPWSTR:
                    array = new string[length];
                    elementDecoder = ptr => Marshal.PtrToStringUni(Marshal.ReadIntPtr(ptr))!;
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_UNKNOWN:
                case VARTYPE.VT_STREAM:
                case VARTYPE.VT_STORAGE:
                    array = new object[length];
                    elementDecoder = ptr => GetObjectForIUnknown(Marshal.ReadIntPtr(ptr));
                    elementSize = IntPtr.Size;
                    break;

                default:
                    throw new NotImplementedException();
            }

            IntPtr elementPtr = variant.Vector.Ptr;
            for (int i = 0; i < length; ++i)
            {
                array.SetValue(elementDecoder.Invoke(elementPtr), i);
                elementPtr += elementSize;
            }

            return array;
        }

        private static object? DecodeValue(PROPVARIANT variant)
        {
            return variant.Type switch
            {
                VARTYPE.VT_BOOL => variant.UI2 != 0,
                VARTYPE.VT_UI1 => variant.UI1,
                VARTYPE.VT_UI2 => variant.UI2,
                VARTYPE.VT_UI4 => variant.UI4,
                VARTYPE.VT_UI8 => variant.UI8,
                VARTYPE.VT_I1 => variant.I1,
                VARTYPE.VT_I2 => variant.I2,
                VARTYPE.VT_I4 => variant.I4,
                VARTYPE.VT_I8 => variant.I8,
                VARTYPE.VT_LPSTR => Marshal.PtrToStringAnsi(variant.Ptr)!,
                VARTYPE.VT_LPWSTR => Marshal.PtrToStringUni(variant.Ptr)!,
                VARTYPE.VT_BSTR => Marshal.PtrToStringBSTR(variant.Ptr),
                VARTYPE.VT_R4 => variant.R4,
                VARTYPE.VT_R8 => variant.R8,
                VARTYPE.VT_FILETIME => DateTime.FromFileTime(variant.I8),
                VARTYPE.VT_UNKNOWN => GetObjectForIUnknown(variant.Ptr),
                VARTYPE.VT_STREAM => GetObjectForIUnknown(variant.Ptr),
                VARTYPE.VT_STORAGE => GetObjectForIUnknown(variant.Ptr),
                _ => throw new NotSupportedException($"Can not decode value of type {variant.Type}.")
            };
        }

        private static object? GetObjectForIUnknown(IntPtr ptr)
        {
            if (ptr == IntPtr.Zero)
            {
                return null;
            }
            return WICComWrappers.Instance.GetOrCreateObjectForComInstance(ptr, CreateObjectFlags.Unwrap);
        }

        private static PROPVARIANT EncodeBlob(WICBlob blob)
        {
            IntPtr memory = Marshal.AllocCoTaskMem(blob.Bytes.Length);
            Marshal.Copy(blob.Bytes, 0, memory, blob.Bytes.Length);

            PROPVARIANT_Vector vector = new PROPVARIANT_Vector()
            {
                Length = blob.Bytes.Length,
                Ptr = memory
            };

            return new PROPVARIANT() { Type = VARTYPE.VT_BLOB, Vector = vector };
        }

        private static PROPVARIANT EncodeVector(Array array, Type elementType)
        {
            if (!elementEncoders.TryGetValue(elementType, out var elementEncoder))
            {
                throw new NotSupportedException($"Can not encode array of {elementType}.");
            }

            int elementSize = elementSizes[elementType];


            IntPtr vectorPtr = Marshal.AllocCoTaskMem(array.Length * elementSize);
            IntPtr elementPtr = vectorPtr;
            foreach (var value in array)
            {
                elementEncoder(elementPtr, value);
                elementPtr += elementSize;
            }

            return new PROPVARIANT()
            {
                Type = VARTYPE.VT_VECTOR | vectorTypes[elementType],
                Vector = new PROPVARIANT_Vector()
                {
                    Length = array.Length,
                    Ptr = vectorPtr
                }
            };
        }

        private static PROPVARIANT EncodeValue(object value)
        {
            return value switch
            {
                bool => new PROPVARIANT() { Type = VARTYPE.VT_BOOL, UI2 = (bool)value ? (ushort)1 : (ushort)0 },
                byte => new PROPVARIANT() { Type = VARTYPE.VT_UI1, UI1 = (byte)value },
                ushort => new PROPVARIANT() { Type = VARTYPE.VT_UI2, UI2 = (ushort)value },
                uint => new PROPVARIANT() { Type = VARTYPE.VT_UI4, UI4 = (uint)value },
                ulong => new PROPVARIANT() { Type = VARTYPE.VT_UI8, UI8 = (ulong)value },
                sbyte => new PROPVARIANT() { Type = VARTYPE.VT_I1, I1 = (sbyte)value },
                short => new PROPVARIANT() { Type = VARTYPE.VT_I2, I2 = (short)value },
                int => new PROPVARIANT() { Type = VARTYPE.VT_I4, I4 = (int)value },
                long => new PROPVARIANT() { Type = VARTYPE.VT_I8, I8 = (long)value },
                string => new PROPVARIANT() { Type = VARTYPE.VT_LPWSTR, Ptr = Marshal.StringToCoTaskMemUni((string)value) },
                float => new PROPVARIANT() { Type = VARTYPE.VT_R4, R4 = (float)value },
                double => new PROPVARIANT() { Type = VARTYPE.VT_R8, R8 = (double)value },
                DateTime => new PROPVARIANT() { Type = VARTYPE.VT_FILETIME, I8 = ((DateTime)value).ToFileTime() },
                DateTimeOffset => new PROPVARIANT() { Type = VARTYPE.VT_FILETIME, I8 = ((DateTimeOffset)value).ToFileTime() },
                _ => throw new NotSupportedException($"Can not encode value of type {value.GetType()}.")
            };
        }

        [LibraryImport("Ole32.dll", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int PropVariantClear(ref PROPVARIANT pvar);
    }
}
