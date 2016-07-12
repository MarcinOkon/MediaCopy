using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace MediaCopy
{
    class DatedDirectory
    {
        private static Regex r = new Regex(":");


        DirectoryInfo _directory;
        DateTime _dateTaken;

        public DirectoryInfo Directory
        {
            get
            {
                return _directory;
            }
        }
        public DateTime DateTaken
        {
            get
            {
                return _dateTaken;
            }
        }

        public DatedDirectory(DirectoryInfo directory)
        {
            _directory = directory;
            _dateTaken = GetDateTaken(_directory);
        }

        private DateTime GetDateTaken(DirectoryInfo directory)
        {
            var files = directory.GetFiles().Where(Utils.IsImage);
            if (files.Any())
            {
                var datedFiles = files
                    .Select(file => GetDateTakenFromImage(file.FullName))
                    .Where(dateTime => dateTime > DateTime.MinValue);
                if (datedFiles.Any())
                {
                    return datedFiles.Min();
                }

            }
            return DateTime.MinValue;
        }



        private static DateTime GetDateTakenFromImage(string path)
        {
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false))
                {
                    return GetDateTaken(path, myImage);

                }
            }
            catch (Exception)
            {
                var modifiedDate = File.GetLastWriteTime(path);
                Messenger.Display(ConsoleColor.Yellow, "Falling back to creation Date for '{0}' -> {1}", path, modifiedDate.ToShortDateString());
                return modifiedDate;
            }
        }

        private static DateTime GetDateTaken(string path, Image myImage)
        {
            PropertyItem propItem = myImage.GetPropertyItem(36867);
            string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
            var dateTime = DateTime.Parse(dateTaken);
            Messenger.Display(ConsoleColor.DarkGray, "Checked Date Taken for '{0}' -> {1}", path, dateTime.ToShortDateString());
            return dateTime;
        }

    }
}
