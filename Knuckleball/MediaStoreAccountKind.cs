// -----------------------------------------------------------------------
// <copyright file="MediaStoreAccountKind.cs" company="Knuckleball Project">
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
    /// Indicates the type of iTunes Music Store account with which this file was purchased.
    /// </summary>
    [SuppressMessage("Microsoft.Design", "CA1028:EnumStorageShouldBeInt32", Justification = "MediaStoreAccountKind is a byte in the external API.")]
    public enum MediaStoreAccountKind : byte
    {
        /// <summary>
        /// Indicates the account type was not set in this file.
        /// </summary>
        NotSet = byte.MaxValue,

        /// <summary>
        /// Indicates the file was purchased with an Apple iTunes account.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", Justification = "iTunes is a proper name and is spelled correctly.")]
        iTunes = 0,

        /// <summary>
        /// Indicates the file was purchased with an AOL iTunes account.
        /// </summary>
        Aol = 1
    }
}
