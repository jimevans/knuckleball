// -----------------------------------------------------------------------
// <copyright file="FileUtilities.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Knuckleball.Tests
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestUtilities
    {
        private const string TestFilesDirectoryName = "TestFiles";

        public static void CopyTestFile(string fileName, string newFileName)
        {
            string testFileDirectory = GetTestFileDirectory();
            string srcFilePath = Path.Combine(testFileDirectory, Path.GetFileName(fileName));
            string destFilePath = Path.Combine(testFileDirectory, Path.GetFileName(newFileName));
            File.Copy(srcFilePath, destFilePath, true);
        }

        public static string GetTestFileDirectory()
        {
            Assembly executingAssembly = Assembly.GetCallingAssembly();
            string currentDirectory = executingAssembly.Location;

            // If we're shadow copying, fiddle with 
            // the codebase instead 
            if (AppDomain.CurrentDomain.ShadowCopyFiles)
            {
                Uri uri = new Uri(executingAssembly.CodeBase);
                currentDirectory = uri.LocalPath;
            }

            return Path.Combine(Path.GetDirectoryName(currentDirectory), TestFilesDirectoryName);
        }

        public static string ComputeHash(Image image, ImageFormat format)
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
