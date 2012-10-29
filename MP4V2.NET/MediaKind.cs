// -----------------------------------------------------------------------
// <copyright file="MediaType.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MP4V2.NET
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public enum MediaKind
    {
        NotSet = -1,
        Unknown = 0,
        Music = 1,
        Audiobook = 2,
        MusicVideo = 6,
        Movie = 9,
        TVShow = 10,
        Booklet = 11,
        Ringtone = 14
    }
}
