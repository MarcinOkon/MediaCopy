using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Scanner
    {
        public static List<DatedDirectory> GetDirectories()
        {
            var allDirectories = GetAllDirectories(Settings.Root);
            if (Settings.StartDate == DateTime.MinValue)
            {
                return allDirectories;
            }
            return allDirectories.Where(dir => dir.DateTaken > Settings.StartDate).ToList();
        }

        private static List<DatedDirectory> GetAllDirectories(DatedDirectory datedDir)
        {
            var dirs = datedDir.Directory.GetDirectories().SelectMany(dir => GetAllDirectories(new DatedDirectory(dir))).ToList();
            dirs.Add(datedDir);
            Messenger.Display(ConsoleColor.Gray, "Scanning directory '{0}'", datedDir.Directory.FullName);
            return dirs;
        }
        public static IEnumerable<FileInfo> GetFiles(DatedDirectory dir)
        {
            if (Settings.CopyOnlyImages)
            {
                return dir.Directory.GetFiles().Where(Utils.IsImage);
            }
            return dir.Directory.GetFiles();
        }

    }
}
