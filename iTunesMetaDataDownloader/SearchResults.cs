using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace iTunesMetaDataDownloader
{
    [JsonObject]
    class SearchResults
    {
        [JsonProperty("resultCount")]
        public int Count { get; set; }
        [JsonProperty("results")]
        public List<TvEpisode> Episodes { get; set; }
    }
}
