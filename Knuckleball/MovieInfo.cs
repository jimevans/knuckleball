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
        private IList<string> cast;
        private IList<string> directors;
        private IList<string> producers;
        private IList<string> screenwriters;

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
        /// Gets a list of cast members for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Cast"/> property is read-only, but can be
        /// modified by the normal methods of a <see cref="IList{T}"/>.
        /// There is a distinction to be drawn between an empty list where
        /// the cast portion of the atom exists in the file, but with no
        /// entries, and the state where the cast portion does not exist
        /// at all in the file. To handle the latter case, use the 
        /// <see cref="RemoveCast"/> method. Note that if the cast portion
        /// of the atom does not exist, accessing the <see cref="Cast"/>
        /// property will create an empty list, adding an empty list to 
        /// that portion of the atom.
        /// </remarks>
        public IList<string> Cast
        {
            get
            {
                if (this.cast == null)
                {
                    this.cast = new List<string>();
                }

                return this.cast;
            }
        }

        /// <summary>
        /// Gets a list of directors for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Directors"/> property is read-only, but can be
        /// modified by the normal methods of a <see cref="IList{T}"/>.
        /// There is a distinction to be drawn between an empty list where
        /// the cast portion of the atom exists in the file, but with no
        /// entries, and the state where the directors portion does not exist
        /// at all in the file. To handle the latter case, use the 
        /// <see cref="RemoveDirectors"/> method. Note that if the directors portion
        /// of the atom does not exist, accessing the <see cref="Directors"/>
        /// property will create an empty list, adding an empty list to 
        /// that portion of the atom.
        /// </remarks>
        public IList<string> Directors
        {
            get
            {
                if (this.directors == null)
                {
                    this.directors = new List<string>();
                }

                return this.directors;
            }
        }

        /// <summary>
        /// Gets a list of producers for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Producers"/> property is read-only, but can be
        /// modified by the normal methods of a <see cref="IList{T}"/>.
        /// There is a distinction to be drawn between an empty list where
        /// the cast portion of the atom exists in the file, but with no
        /// entries, and the state where the producers portion does not exist
        /// at all in the file. To handle the latter case, use the 
        /// <see cref="RemoveProducers"/> method. Note that if the producers portion
        /// of the atom does not exist, accessing the <see cref="Producers"/>
        /// property will create an empty list, adding an empty list to 
        /// that portion of the atom.
        /// </remarks>
        public IList<string> Producers
        {
            get
            {
                if (this.producers == null)
                {
                    this.producers = new List<string>();
                }

                return this.producers;
            }
        }

        /// <summary>
        /// Gets a list of screenwriters for this movie.
        /// </summary>
        /// <remarks>
        /// The <see cref="Screenwriters"/> property is read-only, but can be
        /// modified by the normal methods of a <see cref="IList{T}"/>.
        /// There is a distinction to be drawn between an empty list where
        /// the cast portion of the atom exists in the file, but with no
        /// entries, and the state where the writers portion does not exist
        /// at all in the file. To handle the latter case, use the 
        /// <see cref="RemoveScreenwriters"/> method. Note that if the writers portion
        /// of the atom does not exist, accessing the <see cref="Screenwriters"/>
        /// property will create an empty list, adding an empty list to 
        /// that portion of the atom.
        /// </remarks>
        public IList<string> Screenwriters
        {
            get
            {
                if (this.screenwriters == null)
                {
                    this.screenwriters = new List<string>();
                }

                return this.screenwriters;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Cast"/> property has data, potentially including an empty list.
        /// Returns <see langword="false"/> if the <see cref="Cast"/> property is <see langword="null"/>.
        /// </summary>
        public bool HasCast
        {
            get { return this.cast != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Directors"/> property has data, potentially including an empty list.
        /// Returns <see langword="false"/> if the <see cref="Directors"/> property is <see langword="null"/>.
        /// </summary>
        public bool HasDirectors
        {
            get { return this.directors != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Producers"/> property has data, potentially including an empty list.
        /// Returns <see langword="false"/> if the <see cref="Producers"/> property is <see langword="null"/>.
        /// </summary>
        public bool HasProducers
        {
            get { return this.producers != null; }
        }

        /// <summary>
        /// Gets a value indicating whether the <see cref="Screenwriters"/> property has data, potentially including an empty list.
        /// Returns <see langword="false"/> if the <see cref="Screenwriters"/> property is <see langword="null"/>.
        /// </summary>
        public bool HasScreenwriters
        {
            get { return this.screenwriters != null; }
        }

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
            get { return "iTunMOVI"; }
        }

        /// <summary>
        /// Removes all data from the <see cref="Cast"/> property, causing it to be <see langword="null"/>.
        /// </summary>
        public void RemoveCast()
        {
            this.cast = null;
        }

        /// <summary>
        /// Removes all data from the <see cref="Directors"/> property, causing it to be <see langword="null"/>.
        /// </summary>
        public void RemoveDirectors()
        {
            this.directors = null;
        }

        /// <summary>
        /// Removes all data from the <see cref="Producers"/> property, causing it to be <see langword="null"/>.
        /// </summary>
        public void RemoveProducers()
        {
            this.producers = null;
        }

        /// <summary>
        /// Removes all data from the <see cref="Screenwriters"/> property, causing it to be <see langword="null"/>.
        /// </summary>
        public void RemoveScreenwriters()
        {
            this.screenwriters = null;
        }

        /// <summary>
        /// Populates this <see cref="MovieInfo"/> with the specific data stored in it.
        /// </summary>
        /// <param name="dataBuffer">A byte array containing the iTunes Metadata Format data
        /// used to populate this <see cref="MovieInfo"/>.</param>
        public override void Populate(byte[] dataBuffer)
        {
            Dictionary<string, object> map = null;
            using (MemoryStream stream = new MemoryStream(dataBuffer))
            {
                using (XmlTextReader reader = new XmlTextReader(stream))
                {
                    reader.XmlResolver = null;
                    reader.DtdProcessing = DtdProcessing.Parse;
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

            if (map != null)
            {
                this.cast = ExtractList(map, "cast");
                this.directors = ExtractList(map, "directors");
                this.producers = ExtractList(map, "producers");
                this.screenwriters = ExtractList(map, "screenwriters");
                if (map.ContainsKey("studio"))
                {
                    this.Studio = map["studio"].ToString();
                }
            }
        }

        /// <summary>
        /// Returns the data to be stored in this <see cref="MovieInfo"/> as a byte array.
        /// </summary>
        /// <returns>The byte array containing the data to be stored in the atom.</returns>
        internal override byte[] ToByteArray()
        {
            byte[] buffer = null;
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.NewLineChars = "\n";
            settings.IndentChars = "\t";
            settings.Indent = true;
            using (MemoryStream stream = new MemoryStream())
            {
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    writer.WriteDocType("plist", "-//Apple//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
                    writer.WriteStartElement("plist");
                    writer.WriteAttributeString("version", "1.0");
                    writer.WriteStartElement("dict");
                    if (this.cast != null)
                    {
                        WriteList(writer, this.cast, "cast");
                    }

                    if (this.directors != null)
                    {
                        WriteList(writer, this.directors, "directors");
                    }

                    if (this.producers != null)
                    {
                        WriteList(writer, this.producers, "producers");
                    }

                    if (this.screenwriters != null)
                    {
                        WriteList(writer, this.screenwriters, "screenwriters");
                    }

                    if (this.Studio != null)
                    {
                        writer.WriteElementString("key", "studio");
                        writer.WriteElementString("string", this.Studio);
                    }

                    writer.WriteEndElement();
                    writer.WriteEndElement();
                }

                buffer = stream.ToArray();
            }

            // HACK! The XmlWriter doesn't format things exactly as I'd like
            // so we're going to convert to a string, manipulate the string,
            // then back to a byte array.
            if (buffer != null)
            {
                string xmlString = Encoding.UTF8.GetString(buffer);
                xmlString = xmlString.Replace("encoding=\"utf-8\"", "encoding=\"UTF-8\"").Replace("\n\t", "\n");
                buffer = Encoding.UTF8.GetBytes(xmlString);
            }

            return buffer;
        }

        private static void WriteList(XmlWriter writer, IList<string> list, string listName)
        {
            writer.WriteElementString("key", listName);
            writer.WriteStartElement("array");
            foreach (string listEntry in list)
            {
                writer.WriteStartElement("dict");
                writer.WriteElementString("key", "name");
                writer.WriteElementString("string", listEntry);
                writer.WriteEndElement();
            }

            writer.WriteFullEndElement();
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
