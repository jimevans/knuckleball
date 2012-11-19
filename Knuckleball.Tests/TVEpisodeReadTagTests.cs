// -----------------------------------------------------------------------
// <copyright file="TVEpisodeTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NUnit.Framework;
using System.Security.Cryptography;
using System.Drawing;
using System.Drawing.Imaging;

namespace Knuckleball.Tests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class TVEpisodeReadTagTests
    {
        MP4File file = null;

        [TestFixtureSetUp]
        public void SetUp()
        {
            string directory = TestUtilities.GetTestFileDirectory();
            string fileName = Path.Combine(directory, "TVEpisode.m4v");
            this.file = MP4File.Open(fileName);
        }

        [Test]
        public void ShouldReadTitle()
        {
            Assert.AreEqual("The PuzzleMaster Strikes Back", file.Tags.Title);
        }

        [Test]
        public void ShouldReadArtist()
        {
            Assert.AreEqual("PuzzleQuest", file.Tags.Artist);
        }

        [Test]
        public void ShouldReadAlbum()
        {
            Assert.AreEqual("PuzzleQuest, Season 2", file.Tags.Album);
        }

        [Test]
        public void ShouldReadAlbumArtist()
        {
            Assert.AreEqual("PuzzleQuest", file.Tags.AlbumArtist);
        }

        [Test]
        public void ShouldReadGrouping()
        {
            Assert.IsNull(file.Tags.Grouping);
        }

        [Test]
        public void ShouldReadComposer()
        {
            Assert.IsNull(file.Tags.Composer);
        }

        [Test]
        public void ShouldReadComment()
        {
            Assert.IsNull(file.Tags.Comment);
        }

        [Test]
        public void ShouldReadGenre()
        {
            Assert.AreEqual("Drama", file.Tags.Genre);
        }

        [Test]
        public void ShouldReadTrackNumber()
        {
            Assert.AreEqual(3, file.Tags.TrackNumber);
        }

        [Test]
        public void ShouldReadTotalTracks()
        {
            Assert.AreEqual(0, file.Tags.TotalTracks);
        }

        [Test]
        public void ShouldReadDiskNumber()
        {
            Assert.AreEqual(1, file.Tags.DiscNumber);
        }

        [Test]
        public void ShouldReadTotalDisks()
        {
            Assert.AreEqual(1, file.Tags.TotalDiscs);
        }

        [Test]
        public void ShouldReadTempo()
        {
            Assert.IsNull(file.Tags.Tempo);
        }

        [Test]
        public void ShouldReadIsCompilation()
        {
            Assert.IsFalse(file.Tags.IsCompilation.Value);
        }

        [Test]
        public void ShouldReadDescription()
        {
            Assert.AreEqual("The PuzzleMaster returns to show the world he was not defeated.", file.Tags.Description);
        }

        [Test]
        public void ShouldReadLongDescription()
        {
            Assert.AreEqual("The PuzzleMaster returns to show the world he was not defeated. Planning new diabolical puzzles for the user to find, He has kidnapped the beloved Jimmy Bear.", file.Tags.LongDescription);
        }

        [Test]
        public void ShouldReadLyrics()
        {
            Assert.IsNull(file.Tags.Lyrics);
        }

        [Test]
        public void ShouldReadSortName()
        {
            Assert.AreEqual("PuzzleMaster Strikes Back", file.Tags.SortName);
        }

        [Test]
        public void ShouldReadSortArtist()
        {
            Assert.IsNull(file.Tags.SortArtist);
        }

        [Test]
        public void ShouldReadSortAlbum()
        {
            Assert.AreEqual("PuzzleQuest, Season 2", file.Tags.SortAlbum);
        }

        [Test]
        public void ShouldReadSortAlbumArtist()
        {
            Assert.IsNull(file.Tags.SortAlbumArtist);
        }

        [Test]
        public void ShouldReadSortComposer()
        {
            Assert.IsNull(file.Tags.SortComposer);
        }

        [Test]
        public void ShouldReadSortTVShow()
        {
            Assert.IsNull(file.Tags.SortTVShow);
        }

        [Test]
        public void ShouldReadArtworkCount()
        {
            Assert.AreEqual(1, file.Tags.ArtworkCount);
        }

        [Test]
        public void ShouldReadArtwork()
        {
            Assert.AreEqual(ImageFormat.Jpeg, file.Tags.ArtworkFormat);
            Assert.AreEqual("5527201a6235d454c7e7f724e803e700", TestUtilities.ComputeHash(file.Tags.Artwork, file.Tags.ArtworkFormat));
        }

        [Test]
        public void ShouldReadCopyright()
        {
            Assert.IsNull(file.Tags.Copyright);
        }

        [Test]
        public void ShouldReadEncodingTool()
        {
            Assert.AreEqual("HandBrake 0.9.3 2008112300", file.Tags.EncodingTool);
        }

        [Test]
        public void ShouldReadEncodedBy()
        {
            Assert.IsNull(file.Tags.EncodedBy);
        }

        [Test]
        public void ShouldReadIsPodcast()
        {
            Assert.IsNull(file.Tags.IsPodcast);
        }

        [Test]
        public void ShouldReadKeywords()
        {
            Assert.IsNull(file.Tags.Keywords);
        }

        [Test]
        public void ShouldReadCategory()
        {
            Assert.IsNull(file.Tags.Category);
        }

        [Test]
        public void ShouldReadIsHDVideo()
        {
            Assert.IsFalse(file.Tags.IsHDVideo.Value);
        }

        [Test]
        public void ShouldReadMediaType()
        {
            Assert.AreEqual(MediaKind.TVShow, file.Tags.MediaType);
        }

        [Test]
        public void ShouldReadContentRating()
        {
            Assert.AreEqual(ContentRating.NotSet, file.Tags.ContentRating);
        }

        [Test]
        public void ShouldReadIsGapless()
        {
            Assert.False(file.Tags.IsGapless.Value);
        }
        
        [Test]
        public void ShouldReadMediaStoreAccount()
        {
            Assert.IsNull(file.Tags.MediaStoreAccount);
        }
        
        [Test]
        public void ShouldReadMediaStoreAccountType()
        {
            Assert.AreEqual(MediaStoreAccountKind.NotSet, file.Tags.MediaStoreAccountType);
        }
        
        [Test]
        public void ShouldReadMediaStoreCountry()
        {
            Assert.AreEqual(Country.None, file.Tags.MediaStoreCountry);
        }
        
        [Test]
        public void ShouldReadContentId()
        {
            Assert.AreEqual(0, file.Tags.ContentId.Value);
        }
        
        [Test]
        public void ShouldReadArtistId()
        {
            Assert.IsNull(file.Tags.ArtistId);
        }
        
        [Test]
        public void ShouldReadPlaylistId()
        {
            Assert.IsNull(file.Tags.PlaylistId);
        }
        
        [Test]
        public void ShouldReadGenreId()
        {
            Assert.IsNull(file.Tags.GenreId);
        }
        
        [Test]
        public void ShouldReadComposerId()
        {
            Assert.IsNull(file.Tags.ComposerId);
        }
        
        [Test]
        public void ShouldReadXid()
        {
            Assert.IsNull(file.Tags.Xid);
        }

        [Test]
        public void ShouldReadRatingInfo()
        {
            Assert.AreEqual("us-tv", file.Tags.RatingInfo.RatingSource);
            Assert.AreEqual("TV-14", file.Tags.RatingInfo.Rating);
            Assert.AreEqual(500, file.Tags.RatingInfo.SortValue);
            Assert.IsNullOrEmpty(file.Tags.RatingInfo.RatingAnnotation);
            Assert.AreEqual("us-tv|TV-14|500|", file.Tags.RatingInfo.ToString());
        }

        [Test]
        public void ShouldReadMovieInfo()
        {
            Assert.IsNull(file.Tags.MovieInfo);
        }

        // The following are tags unique to TV Episodes

        [Test]
        public void ShouldReadEpisodeNumber()
        {
            Assert.AreEqual(3, file.Tags.EpisodeNumber);
        }

        [Test]
        public void ShouldReadSeasonNumber()
        {
            Assert.AreEqual(2, file.Tags.SeasonNumber);
        }

        [Test]
        public void ShouldReadEpisodeId()
        {
            Assert.AreEqual("PQS2E3", file.Tags.EpisodeId);
        }

        [Test]
        public void ShouldReadTVShow()
        {
            Assert.AreEqual("PuzzleQuest", file.Tags.TVShow);
        }

        [Test]
        public void ShouldReadTVNetwork()
        {
            Assert.IsNull(file.Tags.TVNetwork);
        }
    }
}
