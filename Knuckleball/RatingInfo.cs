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
using System.Runtime.InteropServices;
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
        /// Gets the meaning of the atom.
        /// </summary>
        internal override string Meaning
        {
            get { return "com.apple.iTunes"; }
        }

        /// <summary>
        /// Gets the name of the atom.
        /// </summary>
        internal override string Name
        {
            get { return "iTunEXTC"; }
        }

        /// <summary>
        /// Returns the string representation of the rating.
        /// </summary>
        /// <returns>The string representation of the rating.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0}|{1}|{2}|{3}", this.RatingSource, this.Rating, this.SortValue, this.RatingAnnotation ?? string.Empty);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="RatingInfo"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether two <see cref="RatingInfo"/> objects have the same value.
        /// </summary>
        /// <param name="obj">Determines whether this instance and a specified object, which
        /// must also be a <see cref="RatingInfo"/> object, have the same value.</param>
        /// <returns><see langword="true"/> if obj is a <see cref="RatingInfo"/> and its value
        /// is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            RatingInfo other = obj as RatingInfo;
            if (other == null)
            {
                return false;
            }

            return this.ToString() == other.ToString();
        }

        /// <summary>
        /// Populates this <see cref="RatingInfo"/> with the specific data stored in it in the referenced file.
        /// </summary>
        /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
        /// used to populate this <see cref="RatingInfo"/>.</param>
        public override void Populate(byte[] dataBuffer)
        {
            string ratingString = Encoding.UTF8.GetString(dataBuffer);
            string[] parts = ratingString.Split('|');
            this.RatingSource = parts[0];
            this.Rating = parts[1];
            this.SortValue = int.Parse(parts[2], CultureInfo.InvariantCulture);
            if (parts.Length > 3)
            {
                this.RatingAnnotation = parts[3];
            }
        }

        /// <summary>
        /// Returns the data to be stored in this <see cref="RatingInfo"/> as a byte array.
        /// </summary>
        /// <returns>The byte array containing the data to be stored in the atom.</returns>
        internal override byte[] ToByteArray()
        {
            string rating = this.ToString();
            byte[] buffer = Encoding.UTF8.GetBytes(rating);
            return buffer;
        }
    }
}
