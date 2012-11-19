// -----------------------------------------------------------------------
// <copyright file="TVEpisodeTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using NUnit.Framework;

namespace Knuckleball.Tests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ChapterTests
    {
        private MP4File file = null;
        private string fileCopy;

        [TestFixtureSetUp]
        public void SetUp()
        {
            CopyNewTestFile();
        }

        private void CopyNewTestFile()
        {
            string directory = TestUtilities.GetTestFileDirectory();
            string fileName = Path.Combine(directory, "Chapter.m4v");
            this.fileCopy = Path.Combine(directory, "ChapterCopy.m4v");
            File.Copy(fileName, this.fileCopy, true);
        }

        [SetUp]
        public void Read()
        {
            this.file = MP4File.Open(fileCopy);
        }

        [Test]
        public void ShouldReadChapters()
        {
            List<Chapter> expectedChapters = new List<Chapter>()
            {
                new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 3", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 4", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 5", Duration = TimeSpan.FromMilliseconds(2996) },
            };

            Assert.That(file.Chapters, Is.EquivalentTo(expectedChapters));
        }

        [Test]
        public void ShouldWriteChapters()
        {
            CopyNewTestFile();
            Read();

            this.file.Chapters[1].Duration = TimeSpan.FromSeconds(7);
            this.file.Chapters.Insert(1, new Chapter() { Title = "Inserted Chapter", Duration = TimeSpan.FromSeconds(8) });

            this.file.Save();
            this.file.Load();

            List<Chapter> expectedChapters = new List<Chapter>()
            {
                new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Inserted Chapter", Duration = TimeSpan.FromSeconds(8) },
                new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(7) },
                new Chapter() { Title = "Chapter 3", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 4", Duration = TimeSpan.FromSeconds(15) },
                new Chapter() { Title = "Chapter 5", Duration = TimeSpan.FromMilliseconds(2996) },
            };

            Assert.That(file.Chapters, Is.EquivalentTo(expectedChapters));
        }
    }
}
