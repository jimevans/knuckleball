// -----------------------------------------------------------------------
// <copyright file="HDTypes.cs" company="Knuckleball Project">
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
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "HDKind is a byte in the external API.")]
    public enum HDKind : byte
    {
 
        /// <summary>
        /// Indicates that the video is SD.
        /// </summary>
        SD = 0,

        /// <summary>
        /// Indicates that the video is 720p.
        /// </summary>
        HD = 1,

        /// <summary>
        /// Indicates that the video is 1080p.
        /// </summary>
        FullHD = 2
    }
}
