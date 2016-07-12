using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Utils
    {
        public static readonly List<string> ImageExtensions = new List<string> { ".jpg", ".jpe", ".bmp", ".gif", ".png" };

        public static bool IsImage(FileInfo file)
        {
            return ImageExtensions.Contains(file.Extension.ToLower());
        }
    }
}
