// -----------------------------------------------------------------------
// <copyright file="MovieInfo.cs" company="Knuckleball Project">
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
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Knuckleball
{
    /// <summary>
    /// The <see cref="MovieInfo"/> class is represents all of the information contained
    /// in the "iTunMOVI" atom. This information includes such items as the cast, directors,
    /// producers, and writers.
    /// </summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "iTunMOVI is the correct name of the atom.")]
    public class MovieInfo : Atom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MovieInfo"/> class.
        /// </summary>
        public MovieInfo()
        {
        }

        /// <summary>
        /// Gets or sets the studio responsible for releasing this movie.
        /// </summary>
        public string Studio { get; set; }

        /// <summary>
        /// Gets or sets a list of cast members for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Cast"/> property is read-write, since not
        /// only can items be added to and removed from the list, but 
        /// the list can also not exist at all in the underlying file.
        /// This is to draw the distinction between an empty list where
        /// the cast portion of the atom exists in the file, but with no
        /// entries, and the state where the cast portion does not exist
        /// at all in the file.
        /// </remarks>
        public IList<string> Cast { get; set; }

        /// <summary>
        /// Gets or sets a list of directors for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Directors"/> property is read-write, since not
        /// only can items be added to and removed from the list, but 
        /// the list can also not exist at all in the underlying file.
        /// This is to draw the distinction between an empty list where
        /// the directors portion of the atom exists in the file, but with no
        /// entries, and the state where the directors portion does not exist
        /// at all in the file.
        /// </remarks>
        public IList<string> Directors { get; set; }

        /// <summary>
        /// Gets or sets a list of producers for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Producers"/> property is read-write, since not
        /// only can items be added to and removed from the list, but 
        /// the list can also not exist at all in the underlying file.
        /// This is to draw the distinction between an empty list where
        /// the producers portion of the atom exists in the file, but with no
        /// entries, and the state where the producers portion does not exist
        /// at all in the file.
        /// </remarks>
        public IList<string> Producers { get; set; }

        /// <summary>
        /// Gets or sets a list of screenwriters for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Screenwriters"/> property is read-write, since not
        /// only can items be added to and removed from the list, but 
        /// the list can also not exist at all in the underlying file.
        /// This is to draw the distinction between an empty list where
        /// the screenwriters portion of the atom exists in the file, but with no
        /// entries, and the state where the screenwriters portion does not exist
        /// at all in the file.
        /// </remarks>
        public IList<string> Screenwriters { get; set; }

        /// <summary>
        /// Populates this <see cref="MovieInfo"/> with the specific data stored in it in the referenced file.
        /// </summary>
        /// <param name="data">The iTunes Metadata Format data used to populate this <see cref="MovieInfo"/>.</param>
        internal override void Populate(NativeMethods.MP4ItmfData data)
        {
            byte[] buffer = data.value.ReadBuffer(data.valueSize);
            Dictionary<string, object> map = null;
            if (data.typeCode == NativeMethods.MP4ItmfBasicType.Utf8)
            {
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    using (XmlTextReader reader = new XmlTextReader(stream))
                    {
                        reader.WhitespaceHandling = WhitespaceHandling.None;
                        while (reader.Name != "plist" && !reader.IsStartElement())
                        {
                            reader.Read();
                        }

                        // Move to the first child of the plist element, which should be a "dict" element.
                        // NOTE: This is a very fragile algorithm based on a specific format of XML contained
                        // in the atom. Any changes to this XML format is likely to break this, severely.
                        reader.Read();
                        map = this.ProcessElement(reader) as Dictionary<string, object>;
                        while (!reader.EOF)
                        {
                            reader.Read();
                        }
                    }
                }
            }

            if (map != null)
            {
                this.Cast = ExtractList(map, "cast");
                this.Directors = ExtractList(map, "directors");
                this.Producers = ExtractList(map, "producers");
                this.Screenwriters = ExtractList(map, "screenwriters");
                if (map.ContainsKey("studio"))
                {
                    this.Studio = map["studio"].ToString();
                }
            }
        }

        private static List<string> ExtractList(Dictionary<string, object> map, string key)
        {
            List<string> list = null;
            if (map.ContainsKey(key))
            {
                List<object> objectList = map[key] as List<object>;
                if (objectList != null)
                {
                    list = new List<string>(objectList.Cast<string>());
                }
            }

            return list;
        }

        private object ProcessElement(XmlTextReader reader)
        {
            object returnValue = null;
            switch (reader.Name)
            {
                case "dict":
                    returnValue = this.ProcessDictionary(reader);
                    break;

                case "array":
                    returnValue = this.ProcessArray(reader);
                    break;

                default:
                    string currentName = reader.Name;
                    returnValue = reader.ReadElementContentAsString();
                    while (reader.Name != currentName && reader.NodeType != XmlNodeType.EndElement)
                    {
                        reader.Read();
                    }

                    break;
            }

            return returnValue;
        }

        private Dictionary<string, object> ProcessDictionary(XmlTextReader reader)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            // Move to first child of dict element
            reader.Read();
            while (reader.Name != "dict" && reader.NodeType != XmlNodeType.EndElement)
            {
                string key = string.Empty;
                object value = null;
                if (reader.Name == "key")
                {
                    key = reader.ReadElementContentAsString();
                    while (!reader.IsStartElement())
                    {
                        reader.Read();
                    }
                }

                value = this.ProcessElement(reader);
                dict[key] = value;
            }

            reader.Read();
            return dict;
        }

        private List<object> ProcessArray(XmlTextReader reader)
        {
            List<object> list = new List<object>();
            string currentName = reader.Name;

            // Move to first child of array element
            reader.Read();
            while (reader.Name != currentName && reader.NodeType != XmlNodeType.EndElement)
            {
                object listElement = this.ProcessElement(reader);

                // If the processed element is a dictionary containing a single key-value pair
                // with the key "name", put the value of that pair in the list, not the actual
                // dictionary.
                Dictionary<string, object> elementDict = listElement as Dictionary<string, object>;
                if (elementDict != null && elementDict.Count == 1 && elementDict.ContainsKey("name"))
                {
                    list.Add(elementDict["name"]);
                }
                else
                {
                    list.Add(listElement);
                }
            }

            reader.Read();
            return list;
        }
    }
}
