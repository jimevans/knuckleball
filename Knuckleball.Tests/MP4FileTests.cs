using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using NUnit.Framework;

namespace Knuckleball.Tests
{
    [TestFixture]
    public class MP4FileTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowNullFileName()
        {
            MP4File file = MP4File.Open(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowEmptyStringFileName()
        {
            MP4File file = MP4File.Open(string.Empty);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldNotAllowFileNameWhichDoesNotExist()
        {
            MP4File file = MP4File.Open(@"C:\This\Path\Does\Not\Exist\Nor\Does\This\File.m4v");
        }
    }
}
