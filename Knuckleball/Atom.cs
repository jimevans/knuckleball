// -----------------------------------------------------------------------
// <copyright file="Atom.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knuckleball
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public abstract class Atom
    {
        internal Atom()
        {
        }

        internal void Initialize(IntPtr pointer)
        {
            NativeMethods.MP4ItmfItemList atomItemList = pointer.ReadStructure<NativeMethods.MP4ItmfItemList>();
            for (int i = 0; i < atomItemList.size; i++)
            {
                IntPtr itemPointer = atomItemList.elements[i];
                NativeMethods.MP4ItmfItem item = itemPointer.ReadStructure<NativeMethods.MP4ItmfItem>();
                NativeMethods.MP4ItmfDataList dataList = item.dataList;
                for (int j = 0; j < dataList.size; j++)
                {
                    IntPtr dataListItemPointer = dataList.elements[i];
                    NativeMethods.MP4ItmfData data = dataListItemPointer.ReadStructure<NativeMethods.MP4ItmfData>();
                    this.Populate(data);
                }
            }

            NativeMethods.MP4ItmfItemListFree(pointer);
        }

        protected abstract void Populate(NativeMethods.MP4ItmfData data);
    }
}
