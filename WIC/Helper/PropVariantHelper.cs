using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
    internal static class PropVariantHelper
    {
        static PropVariantHelper()
        {
            decoders = new Dictionary<VARTYPE, Func<PROPVARIANT, object>>()
            {
                [VARTYPE.VT_BOOL] = variant => variant.UI2 != 0,
                [VARTYPE.VT_UI1] = variant => variant.UI1,
                [VARTYPE.VT_UI2] = variant => variant.UI2,
                [VARTYPE.VT_UI4] = variant => variant.UI4,
                [VARTYPE.VT_UI8] = variant => variant.UI8,
                [VARTYPE.VT_I1] = variant => variant.I1,
                [VARTYPE.VT_I2] = variant => variant.I2,
                [VARTYPE.VT_I4] = variant => variant.I4,
                [VARTYPE.VT_I8] = variant => variant.I8,
                [VARTYPE.VT_LPSTR] = variant => Marshal.PtrToStringAnsi(variant.Ptr),
                [VARTYPE.VT_LPWSTR] = variant => Marshal.PtrToStringUni(variant.Ptr),
                [VARTYPE.VT_BSTR] = variant => Marshal.PtrToStringBSTR(variant.Ptr),
                [VARTYPE.VT_R4] = variant => variant.R4,
                [VARTYPE.VT_R8] = variant => variant.R8,
                [VARTYPE.VT_FILETIME] = variant => DateTime.FromFileTime(variant.I8),
                [VARTYPE.VT_UNKNOWN] = variant => Marshal.GetObjectForIUnknown(variant.Ptr),
                [VARTYPE.VT_STREAM] = variant => Marshal.GetObjectForIUnknown(variant.Ptr),
                [VARTYPE.VT_STORAGE] = variant => Marshal.GetObjectForIUnknown(variant.Ptr),
                [VARTYPE.VT_BLOB] = variant =>
                {
                    byte[] blob = new byte[variant.Vector.Length];
                    if (variant.Vector.Length > 0)
                    {
                        Marshal.Copy(variant.Vector.Ptr, blob, 0, variant.Vector.Length);
                    }
                    return blob;
                },
            };

            encoders = new Dictionary<Type, Func<object, PROPVARIANT>>()
            {
                [typeof(bool)] = value => new PROPVARIANT() { Type = VARTYPE.VT_BOOL, UI2 = (bool)value ? (ushort)1 : (ushort)0 },
                [typeof(byte)] = value => new PROPVARIANT() { Type = VARTYPE.VT_UI1, UI1 = (byte)value },
                [typeof(ushort)] = value => new PROPVARIANT() { Type = VARTYPE.VT_UI2, UI2 = (ushort)value },
                [typeof(uint)] = value => new PROPVARIANT() { Type = VARTYPE.VT_UI4, UI4 = (uint)value },
                [typeof(ulong)] = value => new PROPVARIANT() { Type = VARTYPE.VT_UI8, UI8 = (ulong)value },
                [typeof(sbyte)] = value => new PROPVARIANT() { Type = VARTYPE.VT_I1, I1 = (sbyte)value },
                [typeof(short)] = value => new PROPVARIANT() { Type = VARTYPE.VT_I2, I2 = (short)value },
                [typeof(int)] = value => new PROPVARIANT() { Type = VARTYPE.VT_I4, I4 = (int)value },
                [typeof(long)] = value => new PROPVARIANT() { Type = VARTYPE.VT_I8, I8 = (long)value },
                [typeof(string)] = value => new PROPVARIANT() { Type = VARTYPE.VT_LPWSTR, Ptr = Marshal.StringToCoTaskMemUni((string)value) },
                [typeof(float)] = value => new PROPVARIANT() { Type = VARTYPE.VT_R4, R4 = (float)value },
                [typeof(double)] = value => new PROPVARIANT() { Type = VARTYPE.VT_R8, R8 = (double)value },
                [typeof(DateTime)] = value => new PROPVARIANT() { Type = VARTYPE.VT_FILETIME, I8 = ((DateTime)value).ToFileTime() },
                [typeof(DateTimeOffset)] = value => new PROPVARIANT() { Type = VARTYPE.VT_FILETIME, I8 = ((DateTimeOffset)value).ToFileTime() }
            };

            elementSizes = new Dictionary<Type, int>()
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

            vectorTypes = new Dictionary<Type, VARTYPE>()
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

            elementEncoders = new Dictionary<Type, Action<IntPtr, object>>()
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


            disposers = new Dictionary<VARTYPE, Action<IntPtr>>()
            {
                [VARTYPE.VT_LPSTR] = Marshal.FreeCoTaskMem,
                [VARTYPE.VT_LPWSTR] = Marshal.FreeCoTaskMem,
                [VARTYPE.VT_BSTR] = Marshal.FreeBSTR,
                [VARTYPE.VT_UNKNOWN] = ptr => Marshal.Release(ptr),
                [VARTYPE.VT_STREAM] = ptr => Marshal.Release(ptr),
                [VARTYPE.VT_STORAGE] = ptr => Marshal.Release(ptr)
            };
        }

        private const VARTYPE VectorFlags = VARTYPE.VT_ARRAY | VARTYPE.VT_VECTOR | VARTYPE.VT_BYREF;

        private static readonly IReadOnlyDictionary<VARTYPE, Func<PROPVARIANT, object>> decoders;
        private static readonly IReadOnlyDictionary<Type, Func<object, PROPVARIANT>> encoders;
        private static readonly IReadOnlyDictionary<Type, int> elementSizes;
        private static readonly IReadOnlyDictionary<Type, VARTYPE> vectorTypes;
        private static readonly IReadOnlyDictionary<Type, Action<IntPtr, object>> elementEncoders;
        private static readonly IReadOnlyDictionary<VARTYPE, Action<IntPtr>> disposers;

        public static object Decode(ref PROPVARIANT variant)
        {
            if ((variant.Type & VectorFlags) != 0)
            {
                return DecodeVector(variant);
            }
            else if (decoders.TryGetValue(variant.Type, out var decoder))
            {
                return decoder.Invoke(variant);
            }
            else
            {
                throw new NotSupportedException($"Can not decode value of type {variant.Type}.");
            }
        }

        public static PROPVARIANT Encode(object value)
        {
            var type = value.GetType();

            if (type.IsArray)
            {
                return EncodeVector((Array)value, type.GetElementType());
            }
            if (encoders.TryGetValue(type, out var encoder))
            {
                return encoder(value);
            }
            else
            {
                throw new NotSupportedException($"Can not encode value of type {value.GetType()}.");
            }
        }

        public static void Dispose(ref PROPVARIANT variant)
        {
            if ((variant.Type & VectorFlags) != 0)
            {
                DisposeVector(variant);
            }
            else if (disposers.TryGetValue(variant.Type, out var disposer))
            {
                disposer.Invoke(variant.Ptr);
            }
        }

        private static Array DecodeVector(PROPVARIANT variant)
        {
            Type elementType;
            int elementSize;
            Func<IntPtr, object> elementDecoder;

            switch (variant.Type & ~VectorFlags)
            {
                case VARTYPE.VT_I1:
                    elementType = typeof(sbyte);
                    elementDecoder = ptr => (sbyte)Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_I2:
                    elementType = typeof(short);
                    elementDecoder = ptr => Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_I4:
                    elementType = typeof(int);
                    elementDecoder = ptr => Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_I8:
                    elementType = typeof(long);
                    elementDecoder = ptr => Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_UI1:
                    elementType = typeof(byte);
                    elementDecoder = ptr => Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_UI2:
                    elementType = typeof(ushort);
                    elementDecoder = ptr => (ushort)Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_UI4:
                    elementType = typeof(uint);
                    elementDecoder = ptr => (uint)Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_UI8:
                    elementType = typeof(ulong);
                    elementDecoder = ptr => (ulong)Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_LPSTR:
                    elementType = typeof(string);
                    elementDecoder = ptr => Marshal.PtrToStringAnsi(Marshal.ReadIntPtr(ptr));
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_LPWSTR:
                    elementType = typeof(string);
                    elementDecoder = ptr => Marshal.PtrToStringUni(Marshal.ReadIntPtr(ptr));
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_UNKNOWN:
                case VARTYPE.VT_STREAM:
                case VARTYPE.VT_STORAGE:
                    elementType = typeof(object);
                    elementDecoder = ptr => Marshal.GetObjectForIUnknown(Marshal.ReadIntPtr(ptr));
                    elementSize = IntPtr.Size;
                    break;

                default:
                    throw new NotImplementedException();
            }

            int length = variant.Vector.Length;
            var vector = Array.CreateInstance(elementType, length);
            IntPtr elementPtr = variant.Vector.Ptr;
            for (int i = 0; i < length; ++i)
            {
                vector.SetValue(elementDecoder.Invoke(elementPtr), i);
                elementPtr += elementSize;
            }

            return vector;
        }

        public static PROPVARIANT EncodeVector(Array array, Type elementType)
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

        private static void DisposeVector(PROPVARIANT variant)
        {
            // if necessary, dispose each of the vector's elements:
            if (disposers.TryGetValue(variant.Type & ~VectorFlags, out var disposer))
            {
                IntPtr elementPtr = variant.Vector.Ptr;
                for (int i = 0; i < variant.Vector.Length; i++)
                {
                    IntPtr elementValuePtr = Marshal.ReadIntPtr(elementPtr);
                    disposer.Invoke(elementValuePtr);
                    elementPtr += IntPtr.Size;
                }
            }

            // finally, dispose the vector array itself:
            Marshal.FreeCoTaskMem(variant.Vector.Ptr);
        }
    }
}
