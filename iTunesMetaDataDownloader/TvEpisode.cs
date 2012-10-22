using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace iTunesMetaDataDownloader
{
    [JsonObject]
    class TvEpisode
    {
        [JsonProperty("trackId")]
        public int Id { get; set; }
        [JsonProperty("trackName")]
        public string Title { get; set; }
        [JsonProperty("longDescription")]
        public string LongDescription { get; set; }
        [JsonProperty("releaseDate")]
        public DateTime AirDate { get; set; }
        [JsonProperty("shortDescription")]
        public string ShortDescription { get; set; }
        [JsonProperty("artistName")]
        public string SeriesName { get; set; }
        [JsonProperty("collectionName")]
        public string SeasonName { get; set; }
        [JsonProperty("trackNumber")]
        public int EpisodeNumber { get; set; }
        [JsonProperty("collectionId")]
        public int CollectionId { get; set; }
        [JsonProperty("wrapperType")]
        public string WrapperType { get; set; }
        public string SortName { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Number: {1}, Title: {2}", this.Id, this.EpisodeNumber, this.Title);
        }
    }
}
