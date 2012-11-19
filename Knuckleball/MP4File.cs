// -----------------------------------------------------------------------
// <copyright file="MP4File.cs" company="Knuckleball Project">
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
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// Represents an instance of an MP4 file.
    /// </summary>
    public class MP4File
    {
        private string fileName;
        private MetadataTags metadataTags;
        private ChapterList chapters;

        /// <summary>
        /// Prevents a default instance of the <see cref="MP4File"/> class from being created.
        /// </summary>
        private MP4File()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MP4File"/> class.
        /// </summary>
        /// <param name="fileName">The full path and file name of the file to use.</param>
        private MP4File(string fileName)
        {
            if (string.IsNullOrEmpty(fileName) || !File.Exists(fileName))
            {
                throw new ArgumentException("Must specify a valid file name", "fileName");
            }

            this.fileName = fileName;
        }

        /// <summary>
        /// Gets the list of chapters for this file.
        /// </summary>
        public ChapterList Chapters
        {
            get { return this.chapters; }
        }

        /// <summary>
        /// Gets the metadata tags for this file.
        /// </summary>
        public MetadataTags Tags
        {
            get { return this.metadataTags; }
        }

        /// <summary>
        /// Opens and reads the data for the specified file.
        /// </summary>
        /// <param name="fileName">The full path and file name of the MP4 file to open.</param>
        /// <returns>An <see cref="MP4File"/> object you can use to manipulate file.</returns>
        /// <exception cref="ArgumentException">
        /// Thrown if the specified file name is <see langword="null"/> or the empty string.
        /// </exception>
        public static MP4File Open(string fileName)
        {
            MP4File file = new MP4File(fileName);
            file.Load();
            return file;
        }

        /// <summary>
        /// Loads the metadata for this file.
        /// </summary>
        public void Load()
        {
            IntPtr fileHandle = NativeMethods.MP4Read(this.fileName);
            if (fileHandle != IntPtr.Zero)
            {
                try
                {
                    this.metadataTags = MetadataTags.ReadFromFile(fileHandle);
                    this.chapters = ChapterList.ReadFromFile(fileHandle);
                }
                finally
                {
                    NativeMethods.MP4Close(fileHandle);
                }
            }
        }

        /// <summary>
        /// Saves the edits, if any, to the metadata for this file.
        /// </summary>
        public void Save()
        {
            IntPtr fileHandle = NativeMethods.MP4Modify(this.fileName, 0);
            if (fileHandle != IntPtr.Zero)
            {
                try
                {
                    this.metadataTags.WriteToFile(fileHandle);
                    this.chapters.WriteToFile(fileHandle);
                }
                finally
                {
                    NativeMethods.MP4Close(fileHandle);
                }
            }
        }
    }
}
