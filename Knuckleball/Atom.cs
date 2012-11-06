// -----------------------------------------------------------------------
// <copyright file="Atom.cs" company="Knuckleball Project">
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
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// The <see cref="Atom"/> class is the base class for so-called "reverse-DNS" 
    /// MP4 tag atoms. These are supported only by a low-level API in the MP4V2 library,
    /// and as such, require special handling to read and write.
    /// </summary>
    public abstract class Atom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Atom"/> class.
        /// </summary>
        internal Atom()
        {
        }

        internal virtual NativeMethods.MP4ItmfBasicType DataType 
        {
            get { return NativeMethods.MP4ItmfBasicType.Utf8; }
        }

        internal abstract string Meaning { get; }

        internal abstract string Name { get; }

        /// <summary>
        /// Initializes the <see cref="Atom"/> instance from the specified <see cref="IntPtr"/>
        /// value.
        /// </summary>
        /// <param name="fileHandle">The <see cref="IntPtr"/> file handle of the file from which to read this <see cref="Atom"/>.</param>
        internal bool Initialize(IntPtr fileHandle)
        {
            bool isInitialized = false;
            IntPtr rawAtomPointer = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, this.Meaning, this.Name);
            if (rawAtomPointer != IntPtr.Zero)
            {
                NativeMethods.MP4ItmfItemList atomItemList = rawAtomPointer.ReadStructure<NativeMethods.MP4ItmfItemList>();
                isInitialized = atomItemList.size > 0;
                for (int i = 0; i < atomItemList.size; i++)
                {
                    IntPtr itemPointer = atomItemList.elements[i];
                    NativeMethods.MP4ItmfItem item = itemPointer.ReadStructure<NativeMethods.MP4ItmfItem>();
                    NativeMethods.MP4ItmfDataList dataList = item.dataList;
                    for (int j = 0; j < dataList.size; j++)
                    {
                        IntPtr dataListItemPointer = dataList.elements[i];
                        NativeMethods.MP4ItmfData data = dataListItemPointer.ReadStructure<NativeMethods.MP4ItmfData>();
                        if (data.typeCode == this.DataType)
                        {
                            byte[] dataBuffer = data.value.ReadBuffer(data.valueSize);
                            this.Populate(dataBuffer);
                        }
                    }
                }

                NativeMethods.MP4ItmfItemListFree(rawAtomPointer);
            }

            return isInitialized;
        }

        /// <summary>
        /// Populates this <see cref="Atom"/> with the specific data stored in it.
        /// </summary>
        /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
        /// used to populate this <see cref="Atom"/>.</param>
        internal abstract void Populate(byte[] dataBuffer);

        internal abstract byte[] ToByteArray();
    }
}
