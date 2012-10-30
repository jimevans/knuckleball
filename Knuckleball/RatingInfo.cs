// -----------------------------------------------------------------------
// <copyright file="ExtendedInfoAtom.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Knuckleball
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class RatingInfo : Atom
    {
        public RatingInfo()
        {
        }

        public string Rating { get; set; }
        public string RatingSource { get; set; }
        public int SortValue { get; set; }
        public string RatingAnnotation { get; set; }

        protected override void Populate(NativeMethods.MP4ItmfData data)
        {
            byte[] buffer = data.value.ReadBuffer(data.valueSize);
            if (data.typeCode == NativeMethods.MP4ItmfBasicType.Utf8)
            {
                string ratingString = Encoding.UTF8.GetString(buffer);
                string[] parts = ratingString.Split('|');
                this.RatingSource = parts[0];
                this.Rating = parts[1];
                this.SortValue = int.Parse(parts[2]);
                if (parts.Length > 3)
                {
                    this.RatingAnnotation = parts[3];
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}|{1}|{2}|{3}", this.RatingSource, this.Rating, this.SortValue, this.RatingAnnotation ?? string.Empty);
        }
    }
}
