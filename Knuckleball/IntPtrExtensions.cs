// -----------------------------------------------------------------------
// <copyright file="IntPtrExtensions.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class IntPtrExtensions
    {
        public static int? ReadInt(this IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt32(pointer);
        }

        public static long? ReadLong(this IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt64(pointer);
        }

        public static short? ReadShort(this IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadInt16(pointer);
        }

        public static bool? ReadBoolean(this IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadByte(pointer) != 0;
        }

        public static byte? ReadByte(this IntPtr pointer)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            return Marshal.ReadByte(pointer);
        }

        public static T ReadEnumValue<T, U>(this IntPtr pointer, T defaultValue)
            where T : struct
            where U : struct
        {
            if (pointer == IntPtr.Zero)
            {
                return defaultValue;
            }

            object rawValue;
            if (typeof(U) == typeof(byte))
            {
                rawValue = ReadByte(pointer).Value;
            }
            else if (typeof(U) == typeof(long))
            {
                rawValue = ReadLong(pointer).Value;
            }
            else if (typeof(U) == typeof(short))
            {
                rawValue = ReadShort(pointer).Value;
            }
            else
            {
                rawValue = pointer.ReadInt().Value;
            }

            return (T)Enum.ToObject(typeof(T), rawValue);
        }

        public static T ReadStructure<T>(this IntPtr structPtr)
        {
            if (structPtr == IntPtr.Zero)
            {
                throw new ArgumentNullException("structPointer", "Structures cannot be read from a null pointer (IntPtr.Zero)");
            }

            return (T)Marshal.PtrToStructure(structPtr, typeof(T));
        }

        public static byte[] ReadBuffer(this IntPtr pointer, int bufferLength)
        {
            if (pointer == IntPtr.Zero)
            {
                return null;
            }

            byte[] buffer = new byte[bufferLength];
            Marshal.Copy(pointer, buffer, 0, bufferLength);
            return buffer;
        }
    }
}
