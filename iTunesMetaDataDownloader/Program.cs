using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;

namespace iTunesMetaDataDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetMetadata();

            //string existingFile = @"E:\Projects\DVD Conversion\Fringe Season 4\Copy.m4v";
            string existingFile = @"E:\Projects\DVD Conversion\01 A New Day In the Old Town.m4v";
            IntPtr fileHandle = MP4V2.MP4Modify(existingFile, 0);
            IntPtr tagsPtr = MP4V2.MP4TagsAlloc();
            bool fetched = MP4V2.MP4TagsFetch(tagsPtr, fileHandle);

            MP4V2.MP4Tags tags = (MP4V2.MP4Tags)Marshal.PtrToStructure(tagsPtr, typeof(MP4V2.MP4Tags));
            int epNumber = Marshal.ReadInt32(tags.tvEpisode);
            int season = Marshal.ReadInt32(tags.tvSeason);
            MP4V2.MP4TagDisk diskInfo = (MP4V2.MP4TagDisk)Marshal.PtrToStructure(tags.disk, typeof(MP4V2.MP4TagDisk));
            MP4V2.MP4TagTrack trackInfo = (MP4V2.MP4TagTrack)Marshal.PtrToStructure(tags.track, typeof(MP4V2.MP4TagTrack));
            //MP4V2.MP4TagsSetTVEpisodeID(tagsPtr, "Hello world");
            //MP4V2.MP4TagsSetEncodedBy(tagsPtr, "jimbo the bimbo");
            IntPtr extendedPtr = MP4V2.MP4ItmfGetItemsByMeaning(fileHandle, "com.apple.iTunes", "iTunEXTC");
            MP4V2.MP4ItmfItemList extendedMetaData = (MP4V2.MP4ItmfItemList)Marshal.PtrToStructure(extendedPtr, typeof(MP4V2.MP4ItmfItemList));
            for (int i = 0; i < extendedMetaData.size; i++)
            {
                IntPtr itemPtr = new IntPtr(extendedMetaData.elements.ToInt32() + (i * Marshal.SizeOf(extendedMetaData.elements)));
                MP4V2.MP4ItmfItem item = (MP4V2.MP4ItmfItem)Marshal.PtrToStructure(itemPtr, typeof(MP4V2.MP4ItmfItem));
                MP4V2.MP4ItmfDataList dataList = item.dataList;
                for (int j = 0; j < dataList.size; j++)
                {
                    IntPtr dataListItemPtr = new IntPtr(dataList.elements.ToInt32() + (i * Marshal.SizeOf(extendedMetaData.elements)));
                    MP4V2.MP4ItmfData data = (MP4V2.MP4ItmfData)Marshal.PtrToStructure(dataListItemPtr, typeof(MP4V2.MP4ItmfData));
                    byte[] buffer = new byte[data.valueSize];
                    for (int offset = 0; offset < data.valueSize; offset++)
                    {
                        buffer[offset] = Marshal.ReadByte(data.value, offset);
                    }
                    if (data.typeCode == MP4V2.MP4ItmfBasicType.Utf8)
                    {
                        string dataValue = Encoding.UTF8.GetString(buffer);
                    }
                }
                //MP4V2.MP4ItmfRemoveItem(fileHandle, extendedMetaData.elements);
            }
            MP4V2.MP4ItmfItemListFree(extendedPtr);
            bool stored = MP4V2.MP4TagsStore(tagsPtr, fileHandle);
            MP4V2.MP4TagsFree(tagsPtr);
            MP4V2.MP4Close(fileHandle);
        }

        private static void GetMetadata()
        {
            string xmlTemplate = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<xml>
    <episodedetails>
        <title>{0}</title>
        <season>{1}</season>
        <episode>{2}</episode>
        <outline>{3}</outline>
        <plot>{4}</plot>
        <id>{5}</id>
        <year>{6}</year>
        <genre>Drama</genre>
    </episodedetails>
</xml>";
            Console.Write("Enter the TV Show title: ");
            string showName = Console.ReadLine().Replace(' ', '+');
            Console.Write("Enter the season number: ");
            string season = Console.ReadLine();
            string queryString = string.Format("http://itunes.apple.com/search?term={0}+season+{1}&media=tvShow&entity=tvSeason&attribute=tvSeasonTerm", showName, season);
            WebClient client = new WebClient();
            string seasonQuery = client.DownloadString(queryString);
            SearchResults results = JsonConvert.DeserializeObject<SearchResults>(seasonQuery);
            int seasonId = results.Episodes[0].CollectionId;
            queryString = string.Format("http://itunes.apple.com/lookup?id={0}&entity=tvEpisode", seasonId);
            string episodeQuery = client.DownloadString(queryString);
            SearchResults epResults = JsonConvert.DeserializeObject<SearchResults>(episodeQuery);
            foreach (TvEpisode episode in epResults.Episodes)
            {
                if (episode.WrapperType == "track")
                {
                    string fff = episode.EpisodeNumber.ToString("00");
                    string fileName = Path.Combine(@"E:\Projects\DVD Conversion\Fringe Season 4", episode.EpisodeNumber.ToString("00") + ".nfo");
                    string xmlFile = string.Format(xmlTemplate, episode.Title, season, episode.EpisodeNumber, episode.ShortDescription, episode.LongDescription, episode.Id, episode.AirDate.Year);
                    File.WriteAllText(fileName, xmlFile, Encoding.UTF8);
                    Console.WriteLine(episode.ToString());
                }
            }
            Console.ReadLine();
        }
    }
}
