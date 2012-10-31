// -----------------------------------------------------------------------
// <copyright file="Country.cs" company="Knuckleball Project">
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
    /// Specifies the country from which the file was purchased in the iTunes Store.
    /// </summary>
    public enum Country
    {
        /// <summary>
        /// Indicates the country value was not set.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates that the file was bought in Australia
        /// </summary>
        Australia = 143460,

        /// <summary>
        /// Indicates that the file was bought in Austria
        /// </summary>
        Austria = 143445,

        /// <summary>
        /// Indicates that the file was bought in Belgium
        /// </summary>
        Belgium = 143446,

        /// <summary>
        /// Indicates that the file was bought in Canada
        /// </summary>
        Canada = 143455,

        /// <summary>
        /// Indicates that the file was bought in Denmark
        /// </summary>
        Denmark = 143458,

        /// <summary>
        /// Indicates that the file was bought in Finland
        /// </summary>
        Finland = 143447,

        /// <summary>
        /// Indicates that the file was bought in France
        /// </summary>
        France = 143442,

        /// <summary>
        /// Indicates that the file was bought in Germany
        /// </summary>
        Germany = 143443,

        /// <summary>
        /// Indicates that the file was bought in Greece
        /// </summary>
        Greece = 143448,

        /// <summary>
        /// Indicates that the file was bought in Ireland
        /// </summary>
        Ireland = 143449,

        /// <summary>
        /// Indicates that the file was bought in Italy
        /// </summary>
        Italy = 143450,

        /// <summary>
        /// Indicates that the file was bought in Japan
        /// </summary>
        Japan = 143462,

        /// <summary>
        /// Indicates that the file was bought in Luxembourg
        /// </summary>
        Luxembourg = 143451,

        /// <summary>
        /// Indicates that the file was bought in the Netherlands
        /// </summary>
        Netherlands = 143452,

        /// <summary>
        /// Indicates that the file was bought in New Zealand
        /// </summary>
        NewZealand = 143461,

        /// <summary>
        /// Indicates that the file was bought in Norway
        /// </summary>
        Norway = 143457,

        /// <summary>
        /// Indicates that the file was bought in Portugal
        /// </summary>
        Portugal = 143453,

        /// <summary>
        /// Indicates that the file was bought in Spain
        /// </summary>
        Spain = 143454,

        /// <summary>
        /// Indicates that the file was bought in Sweden
        /// </summary>
        Sweden = 143456,

        /// <summary>
        /// Indicates that the file was bought in Switzerland
        /// </summary>
        Switzerland = 143459,

        /// <summary>
        /// Indicates that the file was bought in the United Kingdom
        /// </summary>
        UnitedKingdom = 143444,

        /// <summary>
        /// Indicates that the file was bought in the United States
        /// </summary>
        UnitedStates = 143441,
    }
}
