using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace WIC
{
    internal static class PropVariantHelpers
    {
        static PropVariantHelpers()
        {
            decoders = new Dictionary<VARTYPE, Func<PROPVARIANT, object>>()
            {
                [VARTYPE.VT_BOOL] = variant => variant.UI2 == 0 ? false : true,
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
                [VARTYPE.VT_VECTOR] = DecodeVector,
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
                [typeof(bool)] = (pointer, value) => Marshal.WriteInt16(pointer, (bool)value ? (short)1 : (short)0),
                [typeof(byte)] = (pointer, value) => Marshal.WriteByte(pointer, (byte)value),
                [typeof(ushort)] = (pointer, value) => Marshal.WriteInt16(pointer, (short)(ushort)value),
                [typeof(uint)] = (pointer, value) => Marshal.WriteInt32(pointer, (int)(uint)value),
                [typeof(ulong)] = (pointer, value) => Marshal.WriteInt64(pointer, (long)(ulong)value),
                [typeof(sbyte)] = (pointer, value) => Marshal.WriteByte(pointer, (byte)(sbyte)value),
                [typeof(short)] = (pointer, value) => Marshal.WriteInt16(pointer, (short)value),
                [typeof(int)] = (pointer, value) => Marshal.WriteInt32(pointer, (int)value),
                [typeof(long)] = (pointer, value) => Marshal.WriteInt64(pointer, (long)value),
                [typeof(string)] = (pointer, value) => Marshal.WriteIntPtr(pointer, 0, Marshal.StringToCoTaskMemUni((string)value)),
                [typeof(float)] = (pointer, value) => Marshal.WriteInt32(pointer, BitConverter.ToInt32(BitConverter.GetBytes((float)value), 0)),
                [typeof(double)] = (pointer, value) => Marshal.WriteInt64(pointer, BitConverter.ToInt64(BitConverter.GetBytes((double)value), 0))
            };

            Action<PROPVARIANT> disposePtr = variant =>
            {
                Marshal.FreeCoTaskMem(variant.Ptr);
            };
            Action<PROPVARIANT> disposeBSTR = variant =>
            {
                Marshal.FreeBSTR(variant.Ptr);
            };
            Action<PROPVARIANT> disposeComObject = variant =>
            {
                Marshal.Release(variant.Ptr);
            };

            disposers = new Dictionary<VARTYPE, Action<PROPVARIANT>>()
            {
                [VARTYPE.VT_LPSTR] = disposePtr,
                [VARTYPE.VT_LPWSTR] = disposePtr,
                [VARTYPE.VT_BSTR] = disposeBSTR,
                [VARTYPE.VT_UNKNOWN] = disposeComObject,
                [VARTYPE.VT_STREAM] = disposeComObject,
                [VARTYPE.VT_STORAGE] = disposeComObject,
                [VARTYPE.VT_VECTOR] = DisposeVector,
            };
        }

        private static readonly IReadOnlyDictionary<VARTYPE, Func<PROPVARIANT, object>> decoders;

        private static readonly IReadOnlyDictionary<Type, Func<object, PROPVARIANT>> encoders;
        private static readonly IReadOnlyDictionary<Type, int> elementSizes;
        private static readonly IReadOnlyDictionary<Type, VARTYPE> vectorTypes;
        private static readonly IReadOnlyDictionary<Type, Action<IntPtr, object>> elementEncoders;

        private static readonly IReadOnlyDictionary<VARTYPE, Action<PROPVARIANT>> disposers;

        public static bool TryDecode<T>(ref PROPVARIANT variant, out T value)
        {
            const VARTYPE flagMask = VARTYPE.VT_ARRAY | VARTYPE.VT_VECTOR | VARTYPE.VT_BYREF;
            bool hasFlag = (variant.Type & flagMask) != 0;
            if ((hasFlag && decoders.TryGetValue(variant.Type & flagMask, out var decoder))
                || decoders.TryGetValue(variant.Type, out decoder))
            {
                value = (T)decoder.Invoke(variant);
                return true;
            }
            else
            {
                value = default(T);
                return false;
            }
        }

        public static PROPVARIANT Encode(object value)
        {
            var type = value.GetType();
            if (type.IsArray)
            {
                return EncodeArray((ICollection)value, type.GetElementType());
            }
            if (encoders.TryGetValue(type, out var encoder))
            {
                return encoder(value);
            }
            else
            {
                throw new NotSupportedException("Value type is not supported");
            }
        }

        public static PROPVARIANT EncodeArray(ICollection array, Type elementType)
        {
            if (!elementEncoders.TryGetValue(elementType, out var elementEncoder))
            {
                throw new NotSupportedException("Array element type is not supported");
            }

            int elementSize = elementSizes[elementType];

            IntPtr vectorPtr = Marshal.AllocCoTaskMem(array.Count * elementSize);
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
                    Length = array.Count,
                    Ptr = vectorPtr
                }
            };
        }

        private static object DecodeVector(PROPVARIANT variant)
        {
            Type elementType;
            int elementSize;
            Func<IntPtr, object> elementDecoder;

            switch (variant.Type & ~VARTYPE.VT_VECTOR)
            {
                case VARTYPE.VT_I1:
                    elementType = typeof(sbyte);
                    elementDecoder = ptr => (object)(sbyte)Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_I2:
                    elementType = typeof(short);
                    elementDecoder = ptr => (object)Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_I4:
                    elementType = typeof(int);
                    elementDecoder = ptr => (object)Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_I8:
                    elementType = typeof(long);
                    elementDecoder = ptr => (object)Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_UI1:
                    elementType = typeof(byte);
                    elementDecoder = ptr => (object)Marshal.ReadByte(ptr);
                    elementSize = 1;
                    break;

                case VARTYPE.VT_UI2:
                    elementType = typeof(ushort);
                    elementDecoder = ptr => (object)(ushort)Marshal.ReadInt16(ptr);
                    elementSize = 2;
                    break;

                case VARTYPE.VT_UI4:
                    elementType = typeof(uint);
                    elementDecoder = ptr => (object)(uint)Marshal.ReadInt32(ptr);
                    elementSize = 4;
                    break;

                case VARTYPE.VT_UI8:
                    elementType = typeof(ulong);
                    elementDecoder = ptr => (object)(ulong)Marshal.ReadInt64(ptr);
                    elementSize = 8;
                    break;

                case VARTYPE.VT_LPSTR:
                    elementType = typeof(string);
                    elementDecoder = Marshal.PtrToStringAnsi;
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_LPWSTR:
                    elementType = typeof(string);
                    elementDecoder = Marshal.PtrToStringUni;
                    elementSize = IntPtr.Size;
                    break;

                case VARTYPE.VT_UNKNOWN:
                case VARTYPE.VT_STREAM:
                case VARTYPE.VT_STORAGE:
                    elementType = typeof(object);
                    elementDecoder = Marshal.GetObjectForIUnknown;
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

        public static void Dispose(ref PROPVARIANT variant)
        {
            const VARTYPE flagMask = VARTYPE.VT_ARRAY | VARTYPE.VT_VECTOR | VARTYPE.VT_BYREF;
            bool hasFlag = (variant.Type & flagMask) != (VARTYPE)0;
            Action<PROPVARIANT> disposer;
            if (disposers.TryGetValue(variant.Type, out disposer)
                || (hasFlag && disposers.TryGetValue(variant.Type & flagMask, out disposer)))
            {
                disposer.Invoke(variant);
            }
            variant = new PROPVARIANT();
        }

        private static void DisposeVector(PROPVARIANT variant)
        {
            Action<IntPtr> elementDisposer = null;

            switch (variant.Type & ~VARTYPE.VT_VECTOR)
            {
                case VARTYPE.VT_BOOL:
                case VARTYPE.VT_I1:
                case VARTYPE.VT_I2:
                case VARTYPE.VT_I4:
                case VARTYPE.VT_I8:
                case VARTYPE.VT_UI1:
                case VARTYPE.VT_UI2:
                case VARTYPE.VT_UI4:
                case VARTYPE.VT_UI8:
                case VARTYPE.VT_R4:
                case VARTYPE.VT_R8:
                    break;

                case VARTYPE.VT_BSTR:
                    elementDisposer = Marshal.FreeBSTR;
                    break;

                case VARTYPE.VT_LPSTR:
                case VARTYPE.VT_LPWSTR:
                    elementDisposer = Marshal.FreeCoTaskMem;
                    break;

                case VARTYPE.VT_UNKNOWN:
                case VARTYPE.VT_STREAM:
                case VARTYPE.VT_STORAGE:
                    elementDisposer = ptr => { Marshal.Release(ptr); };
                    break;

                default:
                    throw new NotImplementedException();
            }

            IntPtr vectorPtr = variant.Vector.Ptr;

            // if necessary, dispose each of the vector's elements:
            if (elementDisposer != null)
            {
                IntPtr elementPtr = vectorPtr;
                for (int i = 0, n = variant.Vector.Length; i < n; ++i)
                {
                    elementDisposer.Invoke(Marshal.ReadIntPtr(elementPtr));
                    elementPtr += IntPtr.Size;
                }
            }

            // finally, dispose the vector array itself:
            Marshal.FreeCoTaskMem(vectorPtr);
        }
    }
}
