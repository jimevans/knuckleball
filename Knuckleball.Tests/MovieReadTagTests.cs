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
    public class MovieReadTagTests
    {
        MP4File file = null;

        [TestFixtureSetUp]
        public void SetUp()
        {
            string directory = TestFileUtilities.GetTestFileDirectory();
            string fileName = Path.Combine(directory, "Movie.m4v");
            this.file = MP4File.Open(fileName);
        }

        [Test]
        public void ShouldReadTitle()
        {
            Assert.AreEqual("PuzzleQuest II: The PuzzleMaster Strikes Back", file.Tags.Title);
        }

        [Test]
        public void ShouldReadArtist()
        {
            Assert.AreEqual("Jim Evans", file.Tags.Artist);
        }

        [Test]
        public void ShouldReadAlbum()
        {
            // TODO: Fix the tags in the TV Episode file
            // Assert.AreEqual("PuzzleQuest, Season 2", file.Tags.Album);
            Assert.IsNull(file.Tags.Album);
        }

        [Test]
        public void ShouldReadAlbumArtist()
        {
            Assert.IsNull(file.Tags.AlbumArtist);
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
            Assert.AreEqual("Horror", file.Tags.Genre);
        }

        [Test]
        public void ShouldReadTrackNumber()
        {
            Assert.IsNull(file.Tags.TrackNumber);
        }

        [Test]
        public void ShouldReadTotalTracks()
        {
            Assert.IsNull(file.Tags.TotalTracks);
        }

        [Test]
        public void ShouldReadDiskNumber()
        {
            Assert.IsNull(file.Tags.DiscNumber);
        }

        [Test]
        public void ShouldReadTotalDisks()
        {
            Assert.IsNull(file.Tags.TotalDiscs);
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
            Assert.IsNull(file.Tags.SortName);
        }

        [Test]
        public void ShouldReadSortArtist()
        {
            Assert.IsNull(file.Tags.SortArtist);
        }

        [Test]
        public void ShouldReadSortAlbum()
        {
            Assert.IsNull(file.Tags.SortAlbum);
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
            Assert.AreEqual(file.Tags.ArtworkFormat, ImageFormat.Png);
            Assert.AreEqual("c1b2758c370db440aa48ff3a7519ff24", ComputeHash(file.Tags.Artwork, file.Tags.ArtworkFormat));
        }

        [Test]
        public void ShouldReadCopyright()
        {
            Assert.AreEqual("Â© 2008 PuzzleMaster Productions", file.Tags.Copyright);
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
            Assert.IsNull(file.Tags.IsHDVideo);
        }

        [Test]
        public void ShouldReadMediaType()
        {
            Assert.AreEqual(MediaKind.Movie, file.Tags.MediaType);
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
            Assert.IsNull(file.Tags.ContentId);
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
        public void ShouldReadRating()
        {
            Assert.AreEqual("mpaa", file.Tags.RatingInfo.RatingSource);
            Assert.AreEqual("PG-13", file.Tags.RatingInfo.Rating);
            Assert.AreEqual(300, file.Tags.RatingInfo.SortValue);
            Assert.IsNullOrEmpty(file.Tags.RatingInfo.RatingAnnotation);
            Assert.AreEqual("mpaa|PG-13|300|", file.Tags.RatingInfo.ToString());
        }

        [Test]
        public void ShouldReadMovieInfo()
        {
            Assert.That(file.Tags.MovieInfo.Cast, Is.EquivalentTo(new List<string>() { "The PuzzleMaster", "Jimmy Bear" }));
            Assert.That(file.Tags.MovieInfo.Directors, Is.EquivalentTo(new List<string>() { "Jim Evans" }));
            Assert.That(file.Tags.MovieInfo.Producers, Is.EquivalentTo(new List<string>() { "Jim Evans", "John Smith" }));
            Assert.That(file.Tags.MovieInfo.Screenwriters, Is.EquivalentTo(new List<string>() { "Herman J. Manciewicz" }));
            Assert.AreEqual("PuzzleMaster Studios", file.Tags.MovieInfo.Studio);
        }

        // The following are tags unique to TV Episodes

        [Test]
        public void ShouldReadEpisodeNumber()
        {
            Assert.IsNull(file.Tags.EpisodeNumber);
        }

        [Test]
        public void ShouldReadSeasonNumber()
        {
            Assert.IsNull(file.Tags.SeasonNumber);
        }

        [Test]
        public void ShouldReadEpisodeId()
        {
            Assert.IsNull(file.Tags.EpisodeId);
        }

        [Test]
        public void ShouldReadTVShow()
        {
            Assert.IsNull(file.Tags.TVShow);
        }

        [Test]
        public void ShouldReadTVNetwork()
        {
            Assert.IsNull(file.Tags.TVNetwork);
        }

        private string ComputeHash(Image image, ImageFormat format)
        {
            byte[] buffer;
            using (MemoryStream stream = new MemoryStream())
            {
                image.Save(stream, format);
                buffer = stream.ToArray();
            }

            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(buffer);
            string hashString = BitConverter.ToString(hash).ToLower().Replace("-", string.Empty);
            return hashString;
        }
    }
}
