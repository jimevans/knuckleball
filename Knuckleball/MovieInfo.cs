// -----------------------------------------------------------------------
// <copyright file="ExtendedInfoAtom.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Xml;

namespace Knuckleball
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class MovieInfo : Atom
    {
        public MovieInfo()
        {
        }

        public string Studio { get; set; }

        public IList<string> Cast { get; set; }
        public IList<string> Directors { get; set; }
        public IList<string> Producers { get; set; }
        public IList<string> Screenwriters { get; set; }

        protected override void Populate(NativeMethods.MP4ItmfData data)
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
                        map = ProcessElement(reader) as Dictionary<string, object>;
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

        private List<string> ExtractList(Dictionary<string, object> map, string key)
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
                    returnValue = ProcessDictionary(reader);
                    break;

                case "array":
                    returnValue = ProcessArray(reader);
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
                value = ProcessElement(reader);
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
                object listElement = ProcessElement(reader);

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
