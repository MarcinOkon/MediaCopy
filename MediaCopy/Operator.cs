using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Operator
    {
        public static void DeleteFiles(IEnumerable<FileInfo> files)
        {
            foreach (FileInfo file in files)
            {
                file.Delete();
                Messenger.Display(ConsoleColor.Yellow, "Deleted '{0}'", file.FullName);
            }
        }

        public static IEnumerable<FileInfo> CopyFiles(List<DatedDirectory> dirs)
        {
            var copiedFiles = new List<FileInfo>();
            var sourceRoot = Settings.Root.Directory.FullName;
            foreach (DatedDirectory dir in dirs)
            {
                var files = Scanner.GetFiles(dir);
                foreach (FileInfo file in files)
                {
                    var relativePath = file.FullName.Replace(sourceRoot, string.Empty);
                    var destination = GetDestination(Settings.Target, relativePath, dir.DateTaken);
                    CopyFile(file, destination);
                    copiedFiles.Add(file);
                }
            }
            return copiedFiles;
        }

        private static void CopyFile(FileInfo file, string destination)
        {
            CreateParentDirectory(destination);
            file.CopyTo(destination, true);
            Messenger.Display(ConsoleColor.Green, "Copied '{0}' to '{1}'", file.FullName, destination);
        }

        private static string GetDestination(string targetFolder, string relativePath, DateTime folderDate)
        {
            var filePath = GetFilePath(relativePath, folderDate);
            return string.Format("{0}\\{1}", targetFolder, filePath);
        }

        private static string GetFilePath(string relativePath, DateTime folderDate)
        {
            var folderPath = GetFolderPath(relativePath);
            if (!Settings.KeepSourcePathTree)
            {
                folderPath = folderPath.Replace("\\", " - ");
                if (Settings.AddDate)
                {
                    var folderDateFormated = folderDate.ToString("yyyy-MM-dd");
                    folderPath = string.Format("{0} - {1}", folderDateFormated, folderPath);
                }
            }
            var fileName = relativePath.Substring(relativePath.LastIndexOf("\\") +1);
            return string.Format("{0}\\{1}", folderPath, fileName);
        }

        private static string GetFolderPath(string root)
        {
            var pathWithoutFirstPart = root.Substring(root.IndexOf('\\') + 1);
            return pathWithoutFirstPart.Substring(0, pathWithoutFirstPart.LastIndexOf('\\'));
        }

        private static void CreateParentDirectory(string path)
        {
            var file = new FileInfo(path);
            Directory.CreateDirectory(file.DirectoryName);
        }
    }
}
