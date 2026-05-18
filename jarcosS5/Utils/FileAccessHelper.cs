using System;
using System.Collections.Generic;
using System.Text;

namespace jarcosS5.Utils
{
    public class FileAccessHelper
    {
        public static string GetFolderPath(string filename)
        {
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
