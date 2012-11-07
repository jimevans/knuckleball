// -----------------------------------------------------------------------
// <copyright file="MediaKind.cs" company="Knuckleball Project">
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
using System.Linq;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// Indicates the kind of media contained in this file.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "MediaKind is a byte in the external API.")]
    public enum MediaKind : byte
    {
        /// <summary>
        /// Indicates the media type is not set in this file.
        /// </summary>
        NotSet = byte.MaxValue,

        /// <summary>
        /// Indicates the media type is unknown.
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Indicates the media type is a music file.
        /// </summary>
        Music = 1,

        /// <summary>
        /// Indicates the media type is an audiobook.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Audiobook is spelled consistently with iTunes.")]
        Audiobook = 2,

        /// <summary>
        /// Indicates the media type is a music video.
        /// </summary>
        MusicVideo = 6,

        /// <summary>
        /// Indicates the media type is a movie.
        /// </summary>
        Movie = 9,

        /// <summary>
        /// Indicates the media type is an episode of a TV show.
        /// </summary>
        TVShow = 10,

        /// <summary>
        /// Indicates the media type is a digital booklet.
        /// </summary>
        Booklet = 11,

        /// <summary>
        /// Indicates the media type is a ringtone.
        /// </summary>
        Ringtone = 14,

        /// <summary>
        /// Indicates the media type is an episode of a podcast.
        /// </summary>
        Podcast = 21,

        /// <summary>
        /// Indicates the media type is an iTunesU file.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "iTunes is a proper name and is spelled correctly.")]
        iTunesU = 23
    }
}
