using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;
using System.Drawing;
using MP4V2.NET;

namespace iTunesMetaDataDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            //GetMetadata();

            //string existingFile = @"E:\Projects\DVD Conversion\Fringe Season 4\Copy.m4v";
            string existingFile = @"C:\Projects\mp4v2_wrapper\TestFiles\01 Pilot.m4v";
            IntPtr fileHandle = NativeMethods.MP4Modify(existingFile, 0);
            IntPtr tagsPtr = NativeMethods.MP4TagsAlloc();
            bool fetched = NativeMethods.MP4TagsFetch(tagsPtr, fileHandle);

            NativeMethods.MP4Tags tags = (NativeMethods.MP4Tags)Marshal.PtrToStructure(tagsPtr, typeof(NativeMethods.MP4Tags));
            int epNumber = Marshal.ReadInt32(tags.tvEpisode);
            int season = Marshal.ReadInt32(tags.tvSeason);
            NativeMethods.MP4TagDisk diskInfo = (NativeMethods.MP4TagDisk)Marshal.PtrToStructure(tags.disk, typeof(NativeMethods.MP4TagDisk));
            NativeMethods.MP4TagTrack trackInfo = (NativeMethods.MP4TagTrack)Marshal.PtrToStructure(tags.track, typeof(NativeMethods.MP4TagTrack));
            NativeMethods.MP4TagArtwork artwork = (NativeMethods.MP4TagArtwork)Marshal.PtrToStructure(tags.artwork, typeof(NativeMethods.MP4TagArtwork));
            byte[] artworkBuffer = new byte[artwork.size];
            Marshal.Copy(artwork.data, artworkBuffer, 0, artwork.size);
            Image artworkImage;
            using (MemoryStream imageStream = new MemoryStream(artworkBuffer))
            {
                artworkImage = Image.FromStream(imageStream);
            }
            //NativeMethods.MP4TagsSetTVEpisodeID(tagsPtr, "Hello world");
            //NativeMethods.MP4TagsSetEncodedBy(tagsPtr, "jimbo the bimbo");
            byte mediaType = Marshal.ReadByte(tags.mediaType);
            IntPtr extendedPtr = NativeMethods.MP4ItmfGetItemsByMeaning(fileHandle, "com.apple.iTunes", "iTunEXTC");
            NativeMethods.MP4ItmfItemList extendedMetaData = (NativeMethods.MP4ItmfItemList)Marshal.PtrToStructure(extendedPtr, typeof(NativeMethods.MP4ItmfItemList));
            for (int i = 0; i < extendedMetaData.size; i++)
            {
                //IntPtr itemPtr = new IntPtr(extendedMetaData.elements.ToInt32() + (i * Marshal.SizeOf(extendedMetaData.elements)));
                IntPtr itemPtr = extendedMetaData.elements[i];
                NativeMethods.MP4ItmfItem item = (NativeMethods.MP4ItmfItem)Marshal.PtrToStructure(itemPtr, typeof(NativeMethods.MP4ItmfItem));
                NativeMethods.MP4ItmfDataList dataList = item.dataList;
                for (int j = 0; j < dataList.size; j++)
                {
                    IntPtr dataListItemPtr = dataList.elements[i];
                    NativeMethods.MP4ItmfData data = (NativeMethods.MP4ItmfData)Marshal.PtrToStructure(dataListItemPtr, typeof(NativeMethods.MP4ItmfData));
                    byte[] buffer = new byte[data.valueSize];
                    Marshal.Copy(data.value, buffer, 0, data.valueSize);
                    if (data.typeCode == NativeMethods.MP4ItmfBasicType.Utf8)
                    {
                        string dataValue = Encoding.UTF8.GetString(buffer);
                    }
                }
                //NativeMethods.MP4ItmfRemoveItem(fileHandle, extendedMetaData.elements);
            }
            NativeMethods.MP4ItmfItemListFree(extendedPtr);
            //bool stored = NativeMethods.MP4TagsStore(tagsPtr, fileHandle);
            NativeMethods.MP4TagsFree(tagsPtr);
            NativeMethods.MP4Close(fileHandle);
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
