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
    public class MP4File : IDisposable
    {
        private string fileName;
        private List<Chapter> chapters = new List<Chapter>();
        private MetadataTags metadataTags;

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
        public IList<Chapter> Chapters
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
                    this.metadataTags = MetadataTags.Read(fileHandle);
                    this.ReadChapters(fileHandle);
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
                    this.metadataTags.Write(fileHandle);
                    this.WriteChapters(fileHandle);
                }
                finally
                {
                    NativeMethods.MP4Close(fileHandle);
                }
            }
        }

        /// <summary>
        /// Releases all managed and unmanaged resources referenced by this instance.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases all managed and unmanaged resources referenced by this instance.
        /// </summary>
        /// <param name="disposing"><see langword="true"/> to dispose of managed and unmanaged resources;
        /// <see cref="false"/> to dispose of only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        private void ReadChapters(IntPtr fileHandle)
        {
            this.chapters.Clear();
            IntPtr chapterListPointer = IntPtr.Zero;
            int chapterCount = 0;
            NativeMethods.MP4ChapterType chapterType = NativeMethods.MP4GetChapters(fileHandle, ref chapterListPointer, ref chapterCount, NativeMethods.MP4ChapterType.Qt);
            if (chapterType != NativeMethods.MP4ChapterType.None && chapterCount != 0)
            {
                IntPtr currentChapterPointer = chapterListPointer;
                for (int i = 0; i < chapterCount; i++)
                {
                    NativeMethods.MP4Chapter currentChapter = currentChapterPointer.ReadStructure<NativeMethods.MP4Chapter>();
                    TimeSpan duration = TimeSpan.FromMilliseconds(currentChapter.duration);
                    string title = Encoding.UTF8.GetString(currentChapter.title);
                    if ((currentChapter.title[0] == 0xFE && currentChapter.title[1] == 0xFF) ||
                        (currentChapter.title[0] == 0xFF && currentChapter.title[1] == 0xFE))
                    {
                        title = Encoding.Unicode.GetString(currentChapter.title);
                    }

                    title = title.Substring(0, title.IndexOf('\0'));
                    this.chapters.Add(new Chapter() { Duration = duration, Title = title });
                    currentChapterPointer = IntPtr.Add(currentChapterPointer, Marshal.SizeOf(currentChapter));
                }
            }
            else
            {
                int timeScale = NativeMethods.MP4GetTimeScale(fileHandle);
                long duration = NativeMethods.MP4GetDuration(fileHandle);
                this.chapters.Add(new Chapter() { Duration = TimeSpan.FromSeconds(duration / timeScale), Title = "Chapter 1" });
            }

            if (chapterListPointer != IntPtr.Zero)
            {
                NativeMethods.MP4Free(chapterListPointer);
            }
        }

        private void WriteChapters(IntPtr fileHandle)
        {
            // Find the first video track, so that we make sure the total duration
            // of the chapters we add does not exceed the length of the file.
            int referenceTrackId = -1;
            for (short i = 0; i < NativeMethods.MP4GetNumberOfTracks(fileHandle, null, 0); i++)
            {
                int currentTrackId = NativeMethods.MP4FindTrackId(fileHandle, i, null, 0);
                string trackType = NativeMethods.MP4GetTrackType(fileHandle, currentTrackId);
                if (trackType == NativeMethods.MP4VideoTrackType)
                {
                    referenceTrackId = currentTrackId;
                    break;
                }
            }

            // If we don't have a video track, then we have an audio file, which has
            // only one track, and we can use it to find the duration.
            referenceTrackId = referenceTrackId <= 0 ? 1 : referenceTrackId;
            long referenceTrackDuration = NativeMethods.MP4ConvertFromTrackDuration(fileHandle, referenceTrackId, NativeMethods.MP4GetTrackDuration(fileHandle, referenceTrackId), NativeMethods.MP4TimeScale.Milliseconds);

            long runningTotal = 0;
            List<NativeMethods.MP4Chapter> nativeChapters = new List<NativeMethods.MP4Chapter>();
            foreach (Chapter chapter in this.chapters)
            {
                NativeMethods.MP4Chapter nativeChapter = new NativeMethods.MP4Chapter();

                // Set the title
                nativeChapter.title = new byte[1024];
                byte[] titleByteArray = Encoding.UTF8.GetBytes(chapter.Title);
                Array.Copy(titleByteArray, nativeChapter.title, titleByteArray.Length);

                // Set the duration, making sure that we only use durations up to
                // the length of the reference track.
                long chapterLength = (long)chapter.Duration.TotalMilliseconds;
                if (runningTotal + chapterLength > referenceTrackDuration)
                {
                    nativeChapter.duration = referenceTrackDuration - runningTotal;
                }
                else
                {
                    nativeChapter.duration = chapterLength;
                }

                runningTotal += chapterLength;
                nativeChapters.Add(nativeChapter);
                if (runningTotal > referenceTrackDuration)
                {
                    break;
                }
            }

            NativeMethods.MP4Chapter[] chapterArray = nativeChapters.ToArray();
            NativeMethods.MP4SetChapters(fileHandle, chapterArray, chapterArray.Length, NativeMethods.MP4ChapterType.Qt);
        }
    }
}
