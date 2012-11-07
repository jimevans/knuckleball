// -----------------------------------------------------------------------
// <copyright file="ContentRating.cs" company="Knuckleball Project">
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
    /// Specifies the value for the content rating of an MP4 file.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "ContentRating is a byte in the external API.")]
    public enum ContentRating : byte
    {
        /// <summary>
        /// Indicates that the value is not set in the file.
        /// </summary>
        NotSet = byte.MaxValue,

        /// <summary>
        /// Indicates the value has been set, but there is no rating for the content of this file.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a value of "clean" has been set for the content of this file.
        /// </summary>
        Clean = 2,

        /// <summary>
        /// Indicates a value of "explicit" has been set for the content of this file.
        /// </summary>
        Explicit = 4
    }
}
