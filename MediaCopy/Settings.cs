using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCopy
{
    class Settings
    {
        private static DatedDirectory _root;
        private static DateTime? _startDate;
        private static string _target;
        private static bool? _copyOnlyImages;
        private static bool? _keepSourcePathTree;
        private static bool? _addDate;

        public static DatedDirectory Root
        {
            get
            {
                if (_root == null)
                {
                    _root = GetRoot();
                }
                return _root;
            } 
        }

        public static DateTime StartDate 
        { 
            get 
            {
                if (_startDate == null)
                {
                    _startDate = GetStartDate();
                }
                return _startDate ?? default(DateTime);
            } 
        }

        public static string Target
        {
            get
            {
                if (_target == null)
                {
                    _target = GetTarget();
                }
                return _target;
            }
        }

        public static bool CopyOnlyImages
        {
            get
            {
                if (_copyOnlyImages == null)
                {
                    _copyOnlyImages = GetCopyOnlyImages();
                }
                return _copyOnlyImages ?? default(bool);
            }
        }
        public static bool KeepSourcePathTree
        {
            get
            {
                if (_keepSourcePathTree == null)
                {
                    _keepSourcePathTree = GetKeepSourcePathTree();
                }
                return _keepSourcePathTree ?? default(bool);
            }
        }

        public static bool AddDate
        {           
            get
            {
                if (_addDate == null)
                {
                    _addDate = GetAddDate();
                }
                return _addDate ?? default(bool);
            }
        }

        private static bool? GetAddDate()
        {
            Messenger.Display(ConsoleColor.Cyan, "Soll das Datum am Beginn des Ordnernamens hinzugefügt werden? (j/n)");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "j":
                    return true;
                case "n":
                    return false;
                default:
                    return GetAddDate();
            }
        }

        private static bool? GetKeepSourcePathTree()
        {
            Messenger.Display(ConsoleColor.Cyan, "Soll die Ursprungs Verzeichnisstruktur beibehalten werden ('Nein' bedeutet die Ordner werden in einen Zielordner kopiert)? (j/n)");
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "j":
                    return true;
                case "n":
                    return false;
                default:
                    return GetKeepSourcePathTree();
            }
        }

        private static bool? GetCopyOnlyImages()
        {
            var imageExtensions = string.Join(",", Utils.ImageExtensions);
            Messenger.Display(ConsoleColor.Cyan, "Sollen nur Bilder kopiert werden (es werden nur: '{0}' kopiert)? (j/n)", imageExtensions);
            var answer = Console.ReadLine();
            switch (answer)
            {
                case "j":
                    return true;
                case "n":
                    return false;
                default:
                    return GetCopyOnlyImages();
            }
        }

        private static DatedDirectory GetRoot()
        {
            Messenger.Display(ConsoleColor.Cyan, "Quellpfad eingeben:");
            var pathInput = Console.ReadLine();
            try
            {
                var dir = new DirectoryInfo(pathInput);
                if (dir.Exists)
                {
                    return new DatedDirectory(dir);
                }
                return GetRootError();
            }
            catch (Exception)
            {
                return GetRootError();
            }
        }

        private static DatedDirectory GetRootError()
        {
            Messenger.Display(ConsoleColor.Red, "Quellpfad ungültig!");
            return GetRoot();
        }

        private static DateTime GetStartDate()
        {
            Messenger.Display(ConsoleColor.Cyan, "Ab welchem Datum sollen die Bild Verzeichnisse kopiert werden (leer lassen für keine Zeitbegrenzung)?");
            var answer = Console.ReadLine();
            if (answer == string.Empty)
            {
                return DateTime.MinValue;
            }
            DateTime startDate = DateTime.MinValue;
            if (DateTime.TryParse(answer, out startDate))
            {
                return startDate;
            }
            Messenger.Display(ConsoleColor.Red, "Datum ungültig!");
            return GetStartDate();
        }

        private static string GetTarget()
        {
            Messenger.Display(ConsoleColor.Cyan, "Ziel Ordner eingeben (zum Beispiel 'X:\\' oder 'Y:\\Fotos'):");
            var targetFolder = Console.ReadLine();

            if (Directory.Exists(targetFolder))
            {
                return targetFolder;
            }
            Messenger.Display(ConsoleColor.Red, "Ziellaufwerk");
            return GetTarget();
        }
    }
}
