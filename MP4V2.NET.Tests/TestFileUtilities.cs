// -----------------------------------------------------------------------
// <copyright file="FileUtilities.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;

namespace MP4V2.NET.Tests
{

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TestFileUtilities
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
    }
}
