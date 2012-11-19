// -----------------------------------------------------------------------
// <copyright file="ChapterListTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace Knuckleball.Tests
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ChapterListTests
    {
        private ChapterList list;

        private bool IsListDirty
        {
            get
            {
                PropertyInfo property = typeof(ChapterList).GetProperty("IsDirty", BindingFlags.Instance | BindingFlags.NonPublic);
                object obj = property.GetValue(this.list, null);
                return obj == null ? false : (bool)obj;
            }
        }

        private void SetDirtyFlag(bool flagValue)
        {
            PropertyInfo property = typeof(ChapterList).GetProperty("IsDirty", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(this.list, flagValue, null);
        }

        [SetUp]
        public void CreateList()
        {
            ConstructorInfo ctor = typeof(ChapterList).GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, Type.DefaultBinder, new Type[0], null);
            this.list = ctor.Invoke(null) as ChapterList;
        }

        [Test]
        public void NewListShouldNotBeFlaggedAsDirty()
        {
            Assert.IsFalse(this.IsListDirty);
        }

        [Test]
        public void IndexerShouldBeAbleToGetChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Chapter retrieved = this.list[0];
            Assert.AreEqual(chapter, retrieved);
        }

        [Test]
        public void IndexerShouldBeAbleToSetChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            this.SetDirtyFlag(false);

            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };
            this.list[0] = chapter2;
            this.list[0].Title = "Chapter 2";
            this.list[0].Duration = TimeSpan.FromSeconds(60);

            Assert.AreEqual("Chapter 2", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[0].Duration);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        public void IndexerShouldBeAbleToSetChapterProperties()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            this.SetDirtyFlag(false);

            this.list[0].Title = "Chapter 2";
            this.list[0].Duration = TimeSpan.FromSeconds(60);

            Assert.AreEqual("Chapter 2", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[0].Duration);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerShouldNotBeAbleToGetChapterWithIndexLessThanZero()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            string foo = this.list[-1].Title;
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void IndexerShouldNotBeAbleToGetChapterWithIndexGreaterThanCountMinusOne()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            string foo = this.list[this.list.Count].Title;
        }

        [Test]
        public void AddShouldBeAbleToAddChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.AreEqual(1, this.list.Count);
            Assert.AreEqual("Chapter 1", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(30), this.list[0].Duration);
            Assert.AreEqual(chapter, this.list[0]);
        }

        [Test]
        public void AddShouldBeAbleToAddSimilarChapters()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 =  new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };

            this.list.Add(chapter);
            this.list.Add(chapter2);
            
            Assert.AreEqual(2, this.list.Count);
            
            Assert.AreEqual("Chapter 1", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(30), this.list[0].Duration);
            Assert.AreEqual(chapter, this.list[0]);
            
            Assert.AreEqual("Chapter 1", this.list[1].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(30), this.list[1].Duration);
            Assert.AreEqual(chapter, this.list[1]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddShouldNotBeAbleToAddTheSameChapterTwice()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            this.list.Add(chapter);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddShouldNotBeAbleToAddNullChapter()
        {
            this.list.Add(null);
        }

        [Test]
        public void AddChapterShouldFlagListAsDirty()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        public void ClearShouldClearList()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            this.list.Clear();
            Assert.AreEqual(0, this.list.Count);
        }

        [Test]
        public void ClearShouldDirtyListIfEntriesExist()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            this.SetDirtyFlag(false);
            this.list.Clear();
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        public void ClearShouldNotDirtyListIfNoEntriesExist()
        {
            this.list.Clear();
            Assert.IsFalse(this.IsListDirty);
        }

        [Test]
        public void ContainsShouldFindExistingChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.IsTrue(this.list.Contains(chapter));
        }

        [Test]
        public void ContainsShouldNotFindMissingChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };
            this.list.Add(chapter);
            Assert.IsFalse(this.list.Contains(chapter2));
        }

        [Test]
        public void ContainsShouldNotFindSimilarButDifferentChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.IsFalse(this.list.Contains(chapter2));
        }

        [Test]
        public void IndexOfShouldFindExistingChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.AreEqual(0, this.list.IndexOf(chapter));
        }

        [Test]
        public void IndexOfShouldNotFindSimilarButDifferentChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Assert.AreEqual(-1, this.list.IndexOf(chapter2));
        }

        [Test]
        public void IndexOfShouldNotFindNonExistantChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };
            this.list.Add(chapter);
            Assert.AreEqual(-1, this.list.IndexOf(chapter2));
        }

        [Test]
        public void InsertShouldBeAbleToInsertChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);

            Chapter chapter2 = new Chapter() { Title = "Chapter 0", Duration = TimeSpan.FromSeconds(60) };
            this.list.Insert(0, chapter2);

            Assert.AreEqual(2, this.list.Count);
            Assert.AreEqual("Chapter 0", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[0].Duration);
            Assert.AreEqual(chapter2, this.list[0]);
        }

        [Test]
        public void InsertShouldBeAbleToInsertSimilarChapters()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };

            this.list.Add(chapter);
            this.list.Insert(0, chapter2);

            Assert.AreEqual(2, this.list.Count);

            Assert.AreEqual("Chapter 1", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(30), this.list[0].Duration);
            Assert.AreEqual(chapter, this.list[0]);

            Assert.AreEqual("Chapter 1", this.list[1].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(30), this.list[1].Duration);
            Assert.AreEqual(chapter, this.list[1]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertShouldNotBeAbleToInsertTheSameChapterTwice()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);
            Chapter chapter2 = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Insert(0, chapter2);
            this.list.Insert(0, chapter2);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InsertShouldNotBeAbleToInsertNullChapter()
        {
            this.list.Insert(0, null);
        }

        [Test]
        public void InsertChapterShouldFlagListAsDirty()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Insert(0, chapter);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertShouldNotBeAbleToUseIndexLessThanZero()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Insert(-1, chapter);
        }

        [Test]
        public void InsertShouldBeAbleToUseIndexEqualToCount()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);

            int count = this.list.Count;
            Chapter chapter2 = new Chapter() { Title = "Chapter 0", Duration = TimeSpan.FromSeconds(60) };
            this.list.Insert(count, chapter2);

            Assert.AreEqual(2, this.list.Count);
            Assert.AreEqual("Chapter 0", this.list[count].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[count].Duration);
            Assert.AreEqual(chapter2, this.list[count]);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void InsertShouldNotBeAbleToUseIndexGreaterThanCount()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Insert(this.list.Count + 1, chapter);
        }

        [Test]
        public void RemoveShouldBeAbleToRemoveChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };

            this.list.Add(chapter);
            this.list.Add(chapter2);

            this.list.Remove(chapter);

            Assert.AreEqual(1, this.list.Count);

            Assert.AreEqual("Chapter 2", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[0].Duration);
            Assert.AreEqual(chapter2, this.list[0]);
        }

        [Test]
        public void RemoveChapterShouldFlagListAsDirty()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };

            this.list.Add(chapter);
            this.list.Add(chapter2);
            this.SetDirtyFlag(false);

            this.list.Remove(chapter);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        public void RemoveShouldBeAbleToRemoveNullChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };

            this.list.Add(chapter);
            this.list.Add(chapter2);

            bool removed = this.list.Remove(null);

            Assert.AreEqual(2, this.list.Count);
            Assert.IsFalse(removed);
        }

        [Test]
        public void RemoveShouldBeAbleToRemoveNonexistentChapter()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };

            this.list.Add(chapter);

            bool removed = this.list.Remove(chapter2);

            Assert.AreEqual(1, this.list.Count);
            Assert.IsFalse(removed);
        }

        [Test]
        public void RemoveOfNonExistentChapterShouldNotFlagListAsDirty()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };

            this.list.Add(chapter);
            this.SetDirtyFlag(false);

            bool removed = this.list.Remove(chapter2);

            Assert.IsFalse(this.IsListDirty);
        }

        [Test]
        public void RemoveAtShouldBeAbleToRemoveChapterAtIndex()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);

            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };
            this.list.Add(chapter2);

            this.list.RemoveAt(0);
            Assert.AreEqual(1, this.list.Count);
            Assert.AreEqual("Chapter 2", this.list[0].Title);
            Assert.AreEqual(TimeSpan.FromSeconds(60), this.list[0].Duration);
            Assert.AreEqual(chapter2, this.list[0]);
        }

        [Test]
        public void RemoveAtShouldFlagListAsDirty()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);

            Chapter chapter2 = new Chapter() { Title = "Chapter 2", Duration = TimeSpan.FromSeconds(60) };
            this.list.Add(chapter2);

            this.SetDirtyFlag(false);
            
            this.list.RemoveAt(0);
            Assert.IsTrue(this.IsListDirty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAtShouldNotBeAbleToRemoveChapterFromIndexLessThanZero()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.RemoveAt(-1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void RemoveAtShouldBeAbleToRemoveChapterFromIndexEqualToCount()
        {
            Chapter chapter = new Chapter() { Title = "Chapter 1", Duration = TimeSpan.FromSeconds(30) };
            this.list.Add(chapter);

            int count = this.list.Count;
            this.list.RemoveAt(count);
        }
    }
}
