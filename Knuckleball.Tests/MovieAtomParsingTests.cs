// -----------------------------------------------------------------------
// <copyright file="MovieAtomParsingTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Knuckleball.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class MovieAtomParsingTests
    {
        private string testAtom = @"<?xml version=1.0 encoding=UTF-8?>
<!DOCTYPE plist http://www.apple.com/DTDs/PropertyList-1.0.dtd>
<plist version=1.0>
  <dict>
    <key>cast</key>
    <array>
      <dict>
        <key>name</key>
        <string>Chris Farley</string>
      </dict>
      <dict>
        <key>name</key>
        <string> David Spade</string>
      </dict>
      <dict>
        <key>name</key>
        <string> Bo Derek</string>
      </dict>
      <dict>
        <key>name</key>
        <string> Brian Dennehy</string>
      </dict>
      <dict>
        <key>name</key>
        <string> Rob Lowe</string>
      </dict>
      <dict>
        <key>name</key>
        <string> Dan Aykroyd</string>
      </dict>
    </array>
    <key>directors</key>
    <array>
      <dict>
        <key>name</key>
        <string>Peter Segal</string>
      </dict>
    </array>
    <key>screenwriters</key>
    <array>
      <dict>
        <key>name</key>
        <string>Peter Segal</string>
      </dict>
    </array>
    <key>producers</key>
    <array>
      <dict>
        <key>name</key>
        <string>Lorne Michaels</string>
      </dict>
    </array>
  </dict> 
</plist>";

        [Test]
        public void ParseTest()
        {
            MovieInfo info = new MovieInfo();
            byte[] data = Encoding.UTF8.GetBytes(this.testAtom);
            info.Populate(data);
        }
    }
}
