using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace MP4V2.NET.Tests
{
    [TestFixture]
    public class MP4FileTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowNullFileName()
        {
            MP4File file = new MP4File(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowEmptyStringFileName()
        {
            MP4File file = new MP4File(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowFileNameWhichDoesNotExist()
        {
            MP4File file = new MP4File(@"C:\This\Path\Does\Not\Exist\Nor\Does\This\File.m4v");
        }
    }
}
