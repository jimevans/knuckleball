// -----------------------------------------------------------------------
// <copyright file="RatingInfo.cs" company="Knuckleball Project">
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
using System.Globalization;
using System.Linq;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// The <see cref="RatingInfo"/> class is represents all of the information contained
    /// in the "iTunEXTC" atom. This information includes information about the parental
    /// advisory ratings of the content.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "iTunEXTC is the correct name of the atom.")]
    public class RatingInfo : Atom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RatingInfo"/> class.
        /// </summary>
        public RatingInfo()
        {
        }

        /// <summary>
        /// Gets or sets the string that represents the rating (e.g., 'PG').
        /// </summary>
        public string Rating { get; set; }

        /// <summary>
        /// Gets or sets the string that represents the source for the rating (e.g., 'mpaa').
        /// </summary>
        public string RatingSource { get; set; }
        
        /// <summary>
        /// Gets or sets the sort value for the rating.
        /// </summary>
        public int SortValue { get; set; }
        
        /// <summary>
        /// Gets or sets the rating annotation.
        /// </summary>
        public string RatingAnnotation { get; set; }

        /// <summary>
        /// Returns the string representation of the rating.
        /// </summary>
        /// <returns>The string representation of the rating.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", this.RatingSource, this.Rating, this.SortValue, this.RatingAnnotation ?? string.Empty);
        }

        /// <summary>
        /// Populates this <see cref="RatingInfo"/> with the specific data stored in it in the referenced file.
        /// </summary>
        /// <param name="data">The iTunes Metadata Format data used to populate this <see cref="RatingInfo"/>.</param>
        internal override void Populate(NativeMethods.MP4ItmfData data)
        {
            byte[] buffer = data.value.ReadBuffer(data.valueSize);
            if (data.typeCode == NativeMethods.MP4ItmfBasicType.Utf8)
            {
                string ratingString = Encoding.UTF8.GetString(buffer);
                string[] parts = ratingString.Split('|');
                this.RatingSource = parts[0];
                this.Rating = parts[1];
                this.SortValue = int.Parse(parts[2], CultureInfo.InvariantCulture);
                if (parts.Length > 3)
                {
                    this.RatingAnnotation = parts[3];
                }
            }
        }
    }
}
