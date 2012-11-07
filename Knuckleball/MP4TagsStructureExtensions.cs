// -----------------------------------------------------------------------
// <copyright file="MP4TagsStructureExtensions.cs" company="Knuckleball Project">
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.
//
// Portions created by Jim Evans are Copyright © 2012.
// All Rights Reserved.
//
// Contributors:
//     Jim Evans, james.h.evans.jr@@gmail.com
//
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// The <see cref="MP4TagsStructureExtensions"/> class contains extension methods used
    /// for marshaling data between managed and unmanaged code using the various API methods
    /// provided by the MP4V2 library.
    /// </summary>
    public static class MP4TagsStructureExtensions
    {
        /// <summary>
        /// Writes a 16-bit integer value to the block of memory pointed to by the specified and
        /// calls the specified API call with a pointer to the block of memory.
        /// </summary>
        /// <param name="tagsStructure">The <see cref="IntPtr"/> value pointing indicating the location of
        /// the MP4Tags structure.</param>
        /// <param name="value">The 16-bit integer value with which to call the MP4V2 API.</param>
        /// <param name="mp4ApiFunction">The MP4V2 API method to call with the pointer to the 16-bit integer value.</param>
        public static void WriteShort(this IntPtr tagsStructure, short? value, Func<IntPtr, IntPtr, bool> mp4ApiFunction)
        {
            if (value == null)
            {
                mp4ApiFunction(tagsStructure, IntPtr.Zero);
            }
            else
            {
                IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(short));
                Marshal.WriteInt16(valuePtr, value.Value);
                mp4ApiFunction(tagsStructure, valuePtr);
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// Writes a 32-bit integer value to the block of memory pointed to by the specified and
        /// calls the specified API call with a pointer to the block of memory.
        /// </summary>
        /// <param name="tagsStructure">The <see cref="IntPtr"/> value pointing indicating the location of
        /// the MP4Tags structure.</param>
        /// <param name="value">The 32-bit integer value with which to call the MP4V2 API.</param>
        /// <param name="mp4ApiFunction">The MP4V2 API method to call with the pointer to the 32-bit integer value.</param>
        public static void WriteInt(this IntPtr tagsStructure, int? value, Func<IntPtr, IntPtr, bool> mp4ApiFunction)
        {
            if (value == null)
            {
                mp4ApiFunction(tagsStructure, IntPtr.Zero);
            }
            else
            {
                IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(int));
                Marshal.WriteInt32(valuePtr, value.Value);
                mp4ApiFunction(tagsStructure, valuePtr);
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// Writes a 64-bit integer value to the block of memory pointed to by the specified and
        /// calls the specified API call with a pointer to the block of memory.
        /// </summary>
        /// <param name="tagsStructure">The <see cref="IntPtr"/> value pointing indicating the location of
        /// the MP4Tags structure.</param>
        /// <param name="value">The 64-bit integer value with which to call the MP4V2 API.</param>
        /// <param name="mp4ApiFunction">The MP4V2 API method to call with the pointer to the 64-bit integer value.</param>
        public static void WriteLong(this IntPtr tagsStructure, long? value, Func<IntPtr, IntPtr, bool> mp4ApiFunction)
        {
            if (value == null)
            {
                mp4ApiFunction(tagsStructure, IntPtr.Zero);
            }
            else
            {
                IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(long));
                Marshal.WriteInt64(valuePtr, value.Value);
                mp4ApiFunction(tagsStructure, valuePtr);
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// Writes an 8-bit integer value to the block of memory pointed to by the specified and
        /// calls the specified API call with a pointer to the block of memory.
        /// </summary>
        /// <param name="tagsStructure">The <see cref="IntPtr"/> value pointing indicating the location of
        /// the MP4Tags structure.</param>
        /// <param name="value">The 8-bit integer value with which to call the MP4V2 API.</param>
        /// <param name="mp4ApiFunction">The MP4V2 API method to call with the pointer to the 8-bit integer value.</param>
        public static void WriteByte(this IntPtr tagsStructure, byte? value, Func<IntPtr, IntPtr, bool> mp4ApiFunction)
        {
            if (value == null)
            {
                mp4ApiFunction(tagsStructure, IntPtr.Zero);
            }
            else
            {
                IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(byte));
                Marshal.WriteByte(valuePtr, value.Value);
                mp4ApiFunction(tagsStructure, valuePtr);
                Marshal.FreeHGlobal(valuePtr);
            }
        }

        /// <summary>
        /// Writes an boolean value as an 8-bit integer to the block of memory pointed to by the
        /// specified and calls the specified API call with a pointer to the block of memory.
        /// </summary>
        /// <param name="tagsStructure">The <see cref="IntPtr"/> value pointing indicating the location of
        /// the MP4Tags structure.</param>
        /// <param name="value">The 8-bit integer value with which to call the MP4V2 API.</param>
        /// <param name="mp4ApiFunction">The MP4V2 API method to call with the pointer to the 8-bit integer value.</param>
        public static void WriteBoolean(this IntPtr tagsStructure, bool? value, Func<IntPtr, IntPtr, bool> mp4ApiFunction)
        {
            if (value == null)
            {
                mp4ApiFunction(tagsStructure, IntPtr.Zero);
            }
            else
            {
                IntPtr valuePtr = Marshal.AllocHGlobal(sizeof(byte));
                byte actualValue = Convert.ToByte(value.Value ? 1 : 0);
                Marshal.WriteByte(valuePtr, actualValue);
                mp4ApiFunction(tagsStructure, valuePtr);
                Marshal.FreeHGlobal(valuePtr);
            }
        }
    }
}
