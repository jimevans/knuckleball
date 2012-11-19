// -----------------------------------------------------------------------
// <copyright file="Chapter.cs" company="Knuckleball Project">
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
using System.Globalization;
using System.Linq;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// Represents a chapter in an MP4 file.
    /// </summary>
    public class Chapter
    {
        private Guid id = Guid.NewGuid();
        private string title = string.Empty;
        private TimeSpan duration = TimeSpan.FromSeconds(0);

        /// <summary>
        /// Occurs when the value of any property is changed.
        /// </summary>
        internal event EventHandler Changed;

        /// <summary>
        /// Gets or sets the title of this chapter.
        /// </summary>
        public string Title 
        {
            get
            {
                return this.title; 
            }

            set
            {
                if (this.title != value)
                {
                    this.title = value;
                    this.OnChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets or sets the duration of this chapter.
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                return this.duration;
            }

            set
            {
                if (this.duration != value)
                {
                    this.duration = value;
                    this.OnChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Gets the internal ID of this Chapter.
        /// </summary>
        internal Guid Id
        {
            get { return this.id; }
        }

        /// <summary>
        /// Returns the string representation of this chapter.
        /// </summary>
        /// <returns>The string representation of the chapter.</returns>
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "{0} ({1} milliseconds)", this.Title, this.Duration.TotalMilliseconds);
        }

        /// <summary>
        /// Returns the hash code for this <see cref="Chapter"/>.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        /// <summary>
        /// Determines whether two <see cref="Chapter"/> objects have the same value.
        /// </summary>
        /// <param name="obj">Determines whether this instance and a specified object, which
        /// must also be a <see cref="Chapter"/> object, have the same value.</param>
        /// <returns><see langword="true"/> if the object is a <see cref="Chapter"/> and its value
        /// is the same as this instance; otherwise, <see langword="false"/>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Chapter other = obj as Chapter;
            if (other == null)
            {
                return false;
            }

            return this.Title == other.Title && this.Duration == other.Duration;
        }

        /// <summary>
        /// Raises the <see cref="Changed"/> event.
        /// </summary>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        protected void OnChanged(EventArgs e)
        {
            if (this.Changed != null)
            {
                this.Changed(this, e);
            }
        }
    }
}
